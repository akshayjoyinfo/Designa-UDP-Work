using Aspose.Cells;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Designa.UDP.Reciever.Service.Persistence;
using Designa.UDP.Reciever.Service.Persistence.Entities;
using Designa.UDP.ReportGenerator.DTO;
using jsreport.Binary;
using jsreport.Local;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Designa.UDP.ReportGenerator
{
    public partial class MainWindow : Form
    {
        private readonly Serilog.ILogger log = Log.Logger;
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly ServiceProvider _serviceProvider;
        private readonly UDPDbContext _context;


        public MainWindow(IConfiguration configuration, ServiceCollection services)
        {
            InitializeComponent();
            _configuration = configuration;
            _services = services;
            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetService<UDPDbContext>();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            log.Information("Loaded MainWindow Report Generator");
            this.fromDatePicker.Value = DateTime.Now.AddDays(-2);
            this.toDatePicker.Value = DateTime.Now.AddDays(-1);
            this.textBox1.Text = _configuration["OutFolder"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            try
            {
                var fromDateString = this.fromDatePicker.Value.ToString("yyyy-MM-dd 00:00:00", CultureInfo.InvariantCulture);
                var toDateString = this.toDatePicker.Value.ToString("yyyy-MM-dd 00:00:00", CultureInfo.InvariantCulture);

                var auditReports = _context.FastagAuditReports
                                .Where(x=> x.ParkingFeeNet > (decimal)0.0 &&
                                           x.DateTimeOfExit >= this.fromDatePicker.Value &&
                                           x.DateTimeOfExit < this.toDatePicker.Value )
                                .ToList();

                var fileName = "FastagReport-"+DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ttt");

                var headerConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,

                };

                var reportHeader= new List<FastagAuditReport>()
                {
                    new FastagAuditReport()
                    {
                        EntryDeviceName = "FASTAG FINANCIAL REPORT"
                    },
                };

                using (var writer = new StreamWriter(_configuration["OutFolder"] + fileName + ".csv"))
                using (var csv = new CsvWriter(writer, headerConfig))
                {
                    var options = new TypeConverterOptions { Formats = new[] { "dd-MM-yyyy HH:mm:ss" } };
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime?>(options);
                    csv.Context.RegisterClassMap<AuditReportHeaderMapper>();
                    csv.WriteRecords(reportHeader);
                }

                var header = new List<FastagAuditReport>()
                {
                    new FastagAuditReport()
                    {
                        ParkingNode = "From",
                        TransactionId = fromDateString,
                    },
                    new FastagAuditReport()
                    {
                        ParkingNode = "To",
                        TransactionId = toDateString,
                        TicketId = "Generated",
                        CardTagNo = DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss:ttt")
                    },

                };

              


                using (var stream = File.Open(_configuration["OutFolder"] + fileName + ".csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, headerConfig))
                {
                    var options = new TypeConverterOptions { Formats = new[] { "dd-MM-yyyy HH:mm:ss" } };
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime?>(options);
                    csv.Context.RegisterClassMap<AuditReportExcelHeaderMapper>();
                    csv.WriteRecords(header);
                }

                var dateConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = true,

                };
                using (var stream = File.Open(_configuration["OutFolder"] + fileName + ".csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, dateConfig))
                {
                    var options = new TypeConverterOptions { Formats = new[] { "dd-MM-yyyy HH:mm:ss" } };
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime?>(options);
                    csv.Context.RegisterClassMap<AuditReportExcelMapper>();
                    csv.WriteRecords(auditReports);
                }

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                var footerRecords = new List<FastagAuditReport>()
                {
                    new FastagAuditReport()
                    {
                        ExitDeviceName = "Total",
                        ModeOfPayment = auditReports.Any()?auditReports.Count.ToString(): "0",
                        ParkingFeeGross = auditReports.Any()?auditReports.Sum(x=>x.ParkingFeeGross):(decimal)0.0,
                        Tax = auditReports.Any()?auditReports.Sum(x=>x.Tax):(decimal)0.0,
                        ParkingFeeNet = auditReports.Any()?auditReports.Sum(x=>x.ParkingFeeNet):(decimal)0.0,
                    }
                };

                using (var stream = File.Open(_configuration["OutFolder"] + fileName + ".csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {
                    var options = new TypeConverterOptions { Formats = new[] { "dd-MM-yyyy HH:mm:ss" } };
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime?>(options);
                    csv.Context.RegisterClassMap<AuditReportExcelFooterMapper>();
                    csv.WriteRecords(footerRecords);
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start("explorer.exe", _configuration["OutFolder"] + fileName + ".csv");
                }

            }
            catch (Exception ex)
            {
                log.Information("Error while downloading Report {ex}", ex);
                MessageBox.Show("Error ocrrued while Generating Reports ", ex.Message);
            }
            finally
            {
                this.button1.Enabled = true;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Supported, Open CSV file MS Excel, you can Export to PDF","ReportGenerator-PDF");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new frmLiveEvents(_configuration, _services).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new frmLaneConfiguration(_configuration, _services).ShowDialog();
        }
    }
}
