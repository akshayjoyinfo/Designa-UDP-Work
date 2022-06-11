using Designa.UDP.Reciever.Service.Application.DTO;
using Designa.UDP.Reciever.Service.Application.DTO.Dial;
using Designa.UDP.Reciever.Service.Application.Shared;
using Designa.UDP.Reciever.Service.Persistence;
using Designa.UDP.Reciever.Service.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Designa.UDP.Reciever.Service.Application.Services
{
    public class UDPMessageService
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Serilog.ILogger log = Log.Logger;
        private readonly ServiceProvider _serviceProvider;
        private readonly UDPDbContext _context;
        private readonly DialSoapClient _dialSoapClient;

        public UDPMessageService(IConfiguration configuration, ServiceCollection services)
        {
            _configuration = configuration;
            _services = services;

            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetService<UDPDbContext>();
            _dialSoapClient = new DialSoapClient(DialSoapClient.EndpointConfiguration.DialSoap, _configuration["DialWebService-Url"]);
        }

        public void ProcessFastagEntryMessage(FastagEntryMessage messageResult, string udpMessage)
        {
            log.Information("Started UDPMessageService : ProcessFastagEntryMessage : {TicketId}", messageResult.FastagEntry.TicketId);

            //save the date in FastagEntry table

            var checkFastagEntry = _context.FastagEntries.FirstOrDefault(x => x.TicketId == messageResult.FastagEntry.TicketId);

            if (checkFastagEntry == null)
            {
                checkFastagEntry = new FastagEntryEntity();
            }
            checkFastagEntry.TicketId = messageResult.FastagEntry.TicketId;
            checkFastagEntry.EntryLaneId = messageResult.FastagEntry.EntryLaneId;
            checkFastagEntry.EntryTime = messageResult.FastagEntry.EntryTime;
            checkFastagEntry.EntryTimeConverted = System.TimeZoneInfo.ConvertTimeFromUtc(
                                                    messageResult.FastagEntry.EntryTime.Value.FromUnixTime(),
                                                    TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            checkFastagEntry.EntryResponseStatusCode = messageResult.FastagEntryRes?.Details.FirstOrDefault().StatusCode.ToString();
            checkFastagEntry.EntryResponseStatusDesc = messageResult.FastagEntryRes?.Details.FirstOrDefault().StatusDesc.ToString();
            checkFastagEntry.VehicleNumber = messageResult.FastagEntryRes?.Details.FirstOrDefault().VehicleNumber.ToString();
            checkFastagEntry.EntryResponseTime = Convert.ToInt64(messageResult.FastagEntryRes?.ResponseTimestamp);
            checkFastagEntry.EntryResponseTimeConverted = System.TimeZoneInfo.ConvertTimeFromUtc(
                                                    checkFastagEntry.EntryResponseTime.Value.FromUnixTime(),
                                                    TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            checkFastagEntry.EntryUdpMessage = udpMessage;
            checkFastagEntry.LastModifiedUtc = DateTime.UtcNow;   

            if(checkFastagEntry.Id == 0)
            {
                _context.FastagEntries.Add(checkFastagEntry);
            }

            _context.SaveChanges();

            // Don''t pubish report yet, wait for paytment to transaction
            //PublishPaymentTranasaction(checkFastagEntry.TicketId);

            log.Information("Completed UDPMessageService : ProcessFastagEntryMessage : {TicketId}", messageResult.FastagEntry.TicketId);
        }

        private FastagAuditReport PublishPaymentTranasaction(string ticketId)
        {
            log.Information("Started UDPMessageService : PublishPaymentTranasaction : {TicketId}", ticketId);
            var fastagEntry = _context.FastagEntries.FirstOrDefault(x => x.TicketId == ticketId);
            var fastPayment = _context.FastagPayments.FirstOrDefault(x => x.TicketId == ticketId);

            var checkFastagTransaction = new FastagAuditReport();

            if (fastPayment!= null && fastPayment.FastagResponseStatusCode =="200")
            {
                
                
                checkFastagTransaction.ParkingNode = _configuration["ParkingNode"];
                checkFastagTransaction.TransactionId = fastPayment.NpciTransactionId;
                checkFastagTransaction.TicketId = fastPayment.TicketId;
                checkFastagTransaction.CardTagNo = fastPayment.EpcId;
                checkFastagTransaction.DateTimeOfEntry = fastPayment.EntryTimeConverted;
                checkFastagTransaction.DateTimeOfExit = fastPayment.ExitTimeConverted;
                checkFastagTransaction.VehicleNumber = fastagEntry?.VehicleNumber;
                checkFastagTransaction.EntryDeviceName = fastPayment.EntryLaneId;
                checkFastagTransaction.ExitDeviceName = fastPayment.ExitLaneId;

                checkFastagTransaction.ModeOfPayment = _configuration["PaymentMode"];

                checkFastagTransaction.ParkingFeeGross = fastPayment.TxnAmount;
                /*
                 * tax 18% calculate from txn_amount tax = (txn_amount - (txn_amount/1.18))
                 */
                var calculated_tax = (fastPayment.TxnAmount - (fastPayment.TxnAmount / (decimal)1.18));
                checkFastagTransaction.Tax = Math.Round(calculated_tax,2);

                /*
                 * Parking net fee calculate from txn_amount =(txn_amount/1.18)
                */

                var calculatedParkingFeeNet = fastPayment.TxnAmount / (decimal)1.18;
                checkFastagTransaction.ParkingFeeNet = Math.Round(calculatedParkingFeeNet, 2);

                checkFastagTransaction.LastModifiedUtc = DateTime.UtcNow;

                var duationSpan = checkFastagTransaction.DateTimeOfExit - checkFastagTransaction.DateTimeOfEntry;

                if(duationSpan != null)
                {
                    var stayDuration = $"{duationSpan?.Days}:{duationSpan?.Hours}:{duationSpan?.Minutes}:{duationSpan?.Seconds}";
                    checkFastagTransaction.StayDuration = stayDuration;
                }

                _context.FastagAuditReports.Add(checkFastagTransaction);
                _context.SaveChanges();

            }
            else
            {
                log.Warning("Fastag API Response is not 200");
            }
            log.Information("Completed UDPMessageService : PublishPaymentTranasaction : {TicketId}", ticketId);
            return checkFastagTransaction;
        }

        public void ProcessFastagPaymentMessage(FastagPaymentMessage messageResult, string udpMessage)
        {
            log.Information("Started UDPMessageService : ProcessFastagPaymentMessage : {TicketId}", messageResult.FastagPayment.TicketId);

            //save the date in FastagPaymentMessage table

            var checkPaymentEntry = new FastagPaymentEntity();
            
            checkPaymentEntry.TicketId = messageResult.FastagPayment.TicketId;
            checkPaymentEntry.EntryLaneId = messageResult.FastagPayment.EntryLaneId;
            checkPaymentEntry.ExitLaneId = messageResult.FastagPayment.ExitLaneId;

            checkPaymentEntry.EntryTime = messageResult.FastagPayment.EntryTime;
            checkPaymentEntry.EntryTimeConverted = System.TimeZoneInfo.ConvertTimeFromUtc(
                                                    checkPaymentEntry.EntryTime.Value.FromUnixTime(),
                                                    TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            checkPaymentEntry.ExitTime = messageResult.FastagPayment.ExitTime;
            checkPaymentEntry.ExitTimeConverted = System.TimeZoneInfo.ConvertTimeFromUtc(
                                                    checkPaymentEntry.ExitTime.Value.FromUnixTime(),
                                                    TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));



            checkPaymentEntry.FastagResponseStatusCode = messageResult.FastagPaymentRes?.StatusCode?.ToString();
            checkPaymentEntry.FastagResponseStatusDesc = messageResult.FastagPaymentRes?.StatusDesc?.ToString();

            checkPaymentEntry.NpciTransactionId = messageResult.FastagPaymentRes?.NpciTransactionId;
            checkPaymentEntry.EpcId = messageResult.FastagPayment.EpcId;

            checkPaymentEntry.TxnAmount = messageResult.FastagPayment.TxnAmount;
            checkPaymentEntry.PaymentUdpMessage = udpMessage;

            checkPaymentEntry.LastModifiedUtc = DateTime.UtcNow;
            _context.FastagPayments.Add(checkPaymentEntry);
            
            _context.SaveChanges();

            var auditPayment = PublishPaymentTranasaction(checkPaymentEntry.TicketId);

            log.Information("Completed UDPMessageService : ProcessFastagPaymentMessage : {TicketId}", messageResult.FastagPayment.TicketId);
            
            if (checkPaymentEntry.FastagResponseStatusCode == "200" && checkPaymentEntry.TxnAmount >0)
            {
                /// Dialweb serice integration


                Task.Run(() =>
                {
                    log.Information("Started UDPMessageService: ProcessFastagPaymentMessage : Sending to Dial WebService : {TicketId}", messageResult.FastagPayment.TicketId);

                    PublishPaymentTransactionToDialWebService(checkPaymentEntry, auditPayment);

                    log.Information("Completed UDPMessageService :ProcessFastagPaymentMessage:  Sending to Dial WebService : {TicketId}", messageResult.FastagPayment.TicketId);
                });

                log.Information("Asynchronosuly sending UDPMessageService: ProcessFastagPaymentMessage : Sending to Dial WebService : {TicketId}", messageResult.FastagPayment.TicketId);
            }
            else
            {
                log.Warning("UDPMessageService : Sending to Dial WebService : Txn Amount it Zero or  FastagResponseStatusCode is not 200 TicketId: {TicketId} FastagResponseStatusCode: {FastagResponseStatusCode}", messageResult.FastagPayment.TicketId,
                    checkPaymentEntry.FastagResponseStatusCode);
            }

            log.Information("DONE UDPMessageService : ProcessFastagPaymentMessage : {TicketId}", messageResult.FastagPayment.TicketId);
        }

        private void PublishPaymentTransactionToDialWebService(FastagPaymentEntity checkPaymentEntry, FastagAuditReport auditPayment)
        {
            // Get Credentials 
            var servicePartneNunber = _configuration["DialWebSerice-ServicePartNumber"].ToString();
            var password = _configuration["DialWebSerice-Password"].ToString();

            var transactionDataPayload = GetTransactionPayload(checkPaymentEntry, auditPayment, (servicePartneNunber, password));
            var response= PublishTransaction(transactionDataPayload);
            log.Information("Dial Response {response}", response);

        }

        private TransactionData GetTransactionPayload(FastagPaymentEntity checkPaymentEntry, FastagAuditReport auditPayment, (string servicePartneNunber, string password) credentials)
        {
            
            var templateXmlPath = _configuration["DialWebService-TemplateDialXmlRequest"].ToString();
            var xmlAsString = File.ReadAllText(templateXmlPath + "\\TransactionDataTemplate.xml");

            TransactionData xmlPayload = null;
            string processedXmlString = string.Empty;
            XmlSerializer serializer = new XmlSerializer(typeof(TransactionData));
            using (StringReader reader = new StringReader(xmlAsString))
            {
                xmlPayload = (TransactionData)serializer.Deserialize(reader);
            }


            // Designa  Cusotm Logic to Transform the Data with Validation Rules
            xmlPayload.ServicePartnerNo = credentials.servicePartneNunber;
            xmlPayload.Password = credentials.password;
            xmlPayload.Transactions.Transaction.TransactionNo = auditPayment.TicketId.Replace("-", "").Trim().Length>20 ? auditPayment.TicketId.Replace("-", "").Trim().Substring(0, 20):auditPayment.TicketId.Replace("-", "").Trim();
            xmlPayload.Transactions.Transaction.OriginalRefNo = auditPayment.TicketId.Replace("-", "").Trim().Length > 20? auditPayment.TicketId.Replace("-", "").Trim().Substring(0, 20): auditPayment.TicketId.Replace("-", "").Trim();
            xmlPayload.Transactions.Transaction.SequenceNumber = auditPayment.Id.ToString();
            //xmlPayload.Transactions.Transaction.EntryType = "1";
            //xmlPayload.Transactions.Transaction.StoreNo = "T2 Car Park";
            //xmlPayload.Transactions.Transaction.POSNo = payment.Qtg_tcc_num.ToString();
            //xmlPayload.Transactions.Transaction.StaffID = "231";
            //xmlPayload.Transactions.Transaction.StaffName = payment.Qtg_tcc_num.ToString().Trim();
            xmlPayload.Transactions.Transaction.TransactionDate = auditPayment.DateTimeOfExit?.ToString("MM-dd-yyyy");
            xmlPayload.Transactions.Transaction.TransactionTime = auditPayment.DateTimeOfExit?.ToString("HH:mm:ss");
            xmlPayload.Transactions.Transaction.NetTransactionAmount = auditPayment.ParkingFeeNet.ToString();
            xmlPayload.Transactions.Transaction.GrossTransactionAmount = auditPayment.ParkingFeeGross.ToString();
            xmlPayload.Transactions.Transaction.Items.Item.BarcodeNo = auditPayment.TicketId.Replace("-", "").Trim().Length > 20 ? auditPayment.TicketId.Replace("-", "").Trim().Substring(0, 20) : auditPayment.TicketId.Replace("-", "").Trim();
            xmlPayload.Transactions.Transaction.Items.Item.Price = auditPayment.ParkingFeeGross.ToString();
            xmlPayload.Transactions.Transaction.Items.Item.NetAmount = auditPayment.ParkingFeeNet.ToString();
            xmlPayload.Transactions.Transaction.Items.Item.TaxAmount = auditPayment.Tax.ToString();
            //var barCodeNo = payment.Qtg_tic_iso.Trim();
            log.Information("Transformed Transactions to Dial webService - GetTransactionPayload");
            return xmlPayload;
        }

        private DialResponse PublishTransaction(TransactionData payment)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TransactionData));
                var processedXmlString = string.Empty;
                DialResponse transationResponse = null;
                string webServiceResponse = null;
                var maxRetryAttempts = 3;
                using (var stringwriter = new System.IO.StringWriter())
                {
                    serializer.Serialize(stringwriter, payment);
                    processedXmlString = stringwriter.ToString();
                }


                processedXmlString = XElement.Parse(processedXmlString).ToString(SaveOptions.DisableFormatting);

                SaveXml(payment.Transactions.Transaction.TransactionNo, DateTime.Now.ToString("dd-MM-yyyy"), processedXmlString);

                Policy.Handle<Exception>(ex =>
                {
                    maxRetryAttempts--;

                    log.Error("Dial WEbService Call: Retrying {Message}", ex.Message);
                    if (maxRetryAttempts == 0)
                    {
                        log.Error("Dial WEbService Call: All 3 retries failed {ex}");
                        return false;
                    }
                    return true;
                })
                 .WaitAndRetry(3, i => TimeSpan.FromSeconds(5)).
                 Execute(() =>
                 {
                     webServiceResponse = _dialSoapClient.SaveTransactionAsync(processedXmlString).GetAwaiter().GetResult();
                   
                 });

                

                XmlSerializer dialSerializer = new XmlSerializer(typeof(DialResponse));
                using (StringReader reader = new StringReader(webServiceResponse))
                {
                    transationResponse = (DialResponse)dialSerializer.Deserialize(reader);
                }
                return transationResponse;
            }catch (Exception ex)
            {
                log.Error("Dial WebService Error : PublishTransaction - Sending XML  -  Dial webService TransactionNo: {TransactionNo} {ex}", payment.Transactions.Transaction.TransactionNo, ex);
                throw;
            }
        }

        private void SaveXml(string transactionNo, string subFolder, string processedXmlString)
        {
            var templateXmlPath = _configuration["DialWebSerice-DialXmlRequestFolder"].ToString();
            var folderPath = templateXmlPath + "\\" + subFolder;

            System.IO.Directory.CreateDirectory(folderPath);
            File.WriteAllText($"{folderPath}\\{transactionNo}_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}.txt", processedXmlString);
        }
    }
}
