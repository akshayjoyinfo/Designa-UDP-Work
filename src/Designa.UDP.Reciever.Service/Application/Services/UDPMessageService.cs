using Designa.UDP.Reciever.Service.Application.DTO;
using Designa.UDP.Reciever.Service.Application.Shared;
using Designa.UDP.Reciever.Service.Persistence;
using Designa.UDP.Reciever.Service.Persistence.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designa.UDP.Reciever.Service.Application.Services
{
    public class UDPMessageService
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Serilog.ILogger log = Log.Logger;
        private readonly ServiceProvider _serviceProvider;
        private readonly UDPDbContext _context;
        public UDPMessageService(IConfiguration configuration, ServiceCollection services)
        {
            _configuration = configuration;
            _services = services;

            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetService<UDPDbContext>();
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
            checkFastagEntry.EntryTimeConverted = messageResult.FastagEntry.EntryTime?.FromUnixTime();
            checkFastagEntry.EntryResponseStatusCode = messageResult.FastagEntryRes?.Details.FirstOrDefault().StatusCode.ToString();
            checkFastagEntry.EntryResponseStatusDesc = messageResult.FastagEntryRes?.Details.FirstOrDefault().StatusDesc.ToString();
            checkFastagEntry.VehicleNumber = messageResult.FastagEntryRes?.Details.FirstOrDefault().VehicleNumber.ToString();
            checkFastagEntry.EntryResponseTime = Convert.ToInt64(messageResult.FastagEntryRes?.ResponseTimestamp);
            checkFastagEntry.EntryResponseTimeConverted = checkFastagEntry.EntryResponseTime?.FromUnixTime();
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

        private void PublishPaymentTranasaction(string ticketId)
        {
            log.Information("Started UDPMessageService : PublishPaymentTranasaction : {TicketId}", ticketId);
            var fastagEntry = _context.FastagEntries.FirstOrDefault(x => x.TicketId == ticketId);
            var fastPayment = _context.FastagPayments.FirstOrDefault(x => x.TicketId == ticketId);


            if(fastPayment!= null && fastPayment.FastagResponseStatusCode =="200")
            {
                var checkFastagTransaction = _context.FastagAuditReports.FirstOrDefault(x => x.TicketId == fastPayment.TicketId);
                if (checkFastagTransaction == null)
                {
                    checkFastagTransaction = new FastagAuditReport();
                }

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
                checkFastagTransaction.Tax = Math.Round(calculated_tax,4);

                /*
                 * Parking net fee calculate from txn_amount =(txn_amount/1.18)
                */

                var calculatedParkingFeeNet = fastPayment.TxnAmount / (decimal)1.18;
                checkFastagTransaction.ParkingFeeNet = Math.Round(calculatedParkingFeeNet, 4);

                checkFastagTransaction.LastModifiedUtc = DateTime.UtcNow;

                var duationSpan = checkFastagTransaction.DateTimeOfExit - checkFastagTransaction.DateTimeOfEntry;

                if(duationSpan != null)
                {
                    var stayDuration = $"{duationSpan?.Days}:{duationSpan?.Hours}:{duationSpan?.Minutes}:{duationSpan?.Seconds}";
                    checkFastagTransaction.StayDuration = stayDuration;
                }


                if (checkFastagTransaction.Id == 0)
                {
                    _context.FastagAuditReports.Add(checkFastagTransaction);
                }
                _context.SaveChanges();

            }
            else
            {
                log.Warning("Fastag API Response is not 200");
            }
            log.Information("Completed UDPMessageService : PublishPaymentTranasaction : {TicketId}", ticketId);

        }

        public void ProcessFastagPaymentMessage(FastagPaymentMessage messageResult, string udpMessage)
        {
            log.Information("Started UDPMessageService : ProcessFastagPaymentMessage : {TicketId}", messageResult.FastagPayment.TicketId);

            //save the date in FastagPaymentMessage table

            var checkPaymentEntry = _context.FastagPayments.FirstOrDefault(x => x.TicketId == messageResult.FastagPayment.TicketId);

            if (checkPaymentEntry == null)
            {
                checkPaymentEntry = new FastagPaymentEntity();
            }
            checkPaymentEntry.TicketId = messageResult.FastagPayment.TicketId;
            checkPaymentEntry.EntryLaneId = messageResult.FastagPayment.EntryLaneId;
            checkPaymentEntry.ExitLaneId = messageResult.FastagPayment.ExitLaneId;

            checkPaymentEntry.EntryTime = messageResult.FastagPayment.EntryTime;
            checkPaymentEntry.EntryTimeConverted = messageResult.FastagPayment?.EntryTime?.FromUnixTime();
            
            checkPaymentEntry.ExitTime = messageResult.FastagPayment.ExitTime;
            checkPaymentEntry.ExitTimeConverted = checkPaymentEntry.ExitTime?.FromUnixTime();


            checkPaymentEntry.FastagResponseStatusCode = messageResult.FastagPaymentRes?.StatusCode?.ToString();
            checkPaymentEntry.FastagResponseStatusDesc = messageResult.FastagPaymentRes?.StatusDesc?.ToString();

            checkPaymentEntry.NpciTransactionId = messageResult.FastagPaymentRes.NpciTransactionId;
            checkPaymentEntry.EpcId = messageResult.FastagPayment.EpcId;

            checkPaymentEntry.TxnAmount = messageResult.FastagPayment.TxnAmount;
            checkPaymentEntry.PaymentUdpMessage = udpMessage;

            checkPaymentEntry.LastModifiedUtc = DateTime.UtcNow;

            if (checkPaymentEntry.Id == 0)
            {
                _context.FastagPayments.Add(checkPaymentEntry);
            }

            _context.SaveChanges();

            PublishPaymentTranasaction(checkPaymentEntry.TicketId);

            log.Information("Completed UDPMessageService : ProcessFastagPaymentMessage : {TicketId}", messageResult.FastagPayment.TicketId);
        }
    }
}
