using CsvHelper;
using CsvHelper.Configuration;
using Designa.UDP.Reciever.Service.Application.Services;
using Designa.UDP.Reciever.Service.Persistence;
using Designa.UDP.Reciever.Service.Persistence.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Designa.UDP.FTPIntegration
{
    public class FtpService
    {
        private readonly Serilog.ILogger log = Log.Logger;
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly ServiceProvider _serviceProvider;
        private readonly UDPDbContext _context;

        public FtpService(IConfiguration configuration, ServiceCollection services)
        {
            _configuration = configuration;
            _services = services;

            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetService<UDPDbContext>();
        }

        public void CreateFtpReports()
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss");

                var encryptedFtpUrl = _configuration["FtpUrl"];
                var encryptedUsername = _configuration["FtpUserName"];
                var encryptedFtpPassword = _configuration["FtpPassword"];
                var FtpUrl = StringCipher.Decrypt(encryptedFtpUrl);
                var FtpUsername = StringCipher.Decrypt(encryptedUsername);
                var FtpPassword = StringCipher.Decrypt(encryptedFtpPassword);


                log.Information("Started Sending FTP Reports on {timestamp}" + timestamp, timestamp);
                var fromDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00", CultureInfo.InvariantCulture);
                var toDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59", CultureInfo.InvariantCulture);


                var fromDate = DateTime.Parse(fromDateString);
                var toDate = DateTime.Parse(toDateString);

                var paymentReports = _context.FastagPayments
                                .Where(x => x.TxnAmount > (decimal)0.0 &&
                                           x.EntryTimeConverted >= fromDate &&
                                           x.ExitTimeConverted < toDate)
                                .ToList();

                var ftpPath = _configuration["FtpFolder"];
                var userId = _configuration["UserId"];
                var location = _configuration["Location"];
                var subLocation = _configuration["SubLocation"];
                var transType = _configuration["TransType"];
                var workShiftNo = FTPFileIdentifierService.GenerateWorkShiftNumberService(ftpPath);

                log.Information("workshiftNo created {workShiftNo}", workShiftNo);
                var fileName = $"{location}-" + $"{subLocation}-" + workShiftNo + "-" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "-tktrcpt";

                var headerConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,

                };

                var reportHeader = new List<FastagPaymentFtpReport>()
                {
                    new FastagPaymentFtpReport()
                    {
                        Location = "ABC"
                    },
                };

                using (var writer = new StreamWriter(ftpPath + workShiftNo + "\\" + fileName + ".csv"))
                using (var csv = new CsvWriter(writer, headerConfig))
                {
                    csv.Context.RegisterClassMap<FastagPaymentFtpExcelHederReportMapper>();
                    csv.WriteRecords(reportHeader);
                }

                var dateConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = true,

                };

                var paymentFtpReports = new List<FastagPaymentFtpReport>();

                paymentReports.ForEach(x =>
                {
                    var ftpReportEntry = new FastagPaymentFtpReport()
                    {
                        Location = location,
                        SubLocation = subLocation,
                        UserId = userId,
                        WorkShiftNo = workShiftNo,
                        TicketNo = x.TicketId.Replace("-", "").Trim().Length > 20 ? x.TicketId.Replace("-", "").Trim().Substring(0, 20) : x.TicketId.Replace("-", "").Trim(),
                        TransType = transType,
                        InDate = x.EntryTimeConverted?.ToString("dd-MM-yyyy"),
                        InTime = x.EntryTimeConverted?.ToString("HH:mm:ss"),
                        RecipetNo = x.NpciTransactionId,
                        PaymentDate = x.ExitTimeConverted?.ToString("dd-MM-yyyy"),
                        PaymentTime = x.ExitTimeConverted?.ToString("HH:mm:ss"),
                        TotalCharges = Math.Round(Convert.ToDouble(x.TxnAmount), 2),
                        Tax = Math.Round(Convert.ToDouble((x.TxnAmount - (x.TxnAmount / (decimal)1.18))), 2),
                        ParkingCharges = Math.Round(Convert.ToDouble(x.TxnAmount), 2) - Math.Round(Convert.ToDouble((x.TxnAmount - (x.TxnAmount / (decimal)1.18))), 2),
                        ExitDate = x.ExitTimeConverted?.ToString("dd-MM-yyyy"),
                        ExitTime = x.ExitTimeConverted?.ToString("HH:mm:ss"),
                        PaymentType = transType

                    };
                    paymentFtpReports.Add(ftpReportEntry);
                });

                using (var stream = File.Open(_configuration["FtpFolder"] + workShiftNo + "\\" + fileName + ".csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, dateConfig))
                {
                    csv.Context.RegisterClassMap<FastagPaymentFtpReportMapper>();
                    csv.WriteRecords(paymentFtpReports);
                }

                log.Information("File Created locally " + _configuration["FtpFolder"] + workShiftNo + "\\" + fileName + ".csv");

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FtpUrl + "/" + fileName + ".csv");
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(FtpUsername, FtpPassword);

                // Copy the contents of the file to the request stream.
                using (FileStream fileStream = File.Open(_configuration["FtpFolder"] + workShiftNo + "\\" + fileName + ".csv", FileMode.Open, FileAccess.Read))
                {
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        fileStream.CopyToAsync(requestStream).GetAwaiter().GetResult();
                        //using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        //{
                        //    Console.WriteLine($"Upload csv to FTP Completed, status {response.StatusDescription}");
                        //}
                        log.Information("Waiting for 10 seconds to publish FTP " + fileName + ".csv");
                        Thread.Sleep(10000);
                    }
                }

                log.Information("File Published to FTP " + fileName + ".csv");
            }catch (Exception ex)
            {
                log.Error("Error while Sending or Creating FTP Reports {ex}" , ex);
            }

            Thread.Sleep(3000);
        }
    }
}
