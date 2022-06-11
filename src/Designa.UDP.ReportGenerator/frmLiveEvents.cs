using Designa.UDP.Reciever.Service.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Designa.UDP.ReportGenerator
{
    public partial class frmLiveEvents : Form
    {
        private readonly Serilog.ILogger log = Log.Logger;
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly ServiceProvider _serviceProvider;
        private readonly UDPDbContext _context;

        public frmLiveEvents(IConfiguration configuration, ServiceCollection services)
        {
            InitializeComponent();
            _configuration = configuration;
            _services = services;
            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetService<UDPDbContext>();
        }

        private void frmLiveEvents_Load(object sender, EventArgs e)
        {
            log.Information("Loaded LiveEvent Form");
            var count = _configuration["LiveEventsFeedRowCount"] != null ? Convert.ToInt32(_configuration["LiveEventsFeedRowCount"]) : 100;
            LoadEvents(count);
            label1.Text = "Last Refreshed at " + DateTime.Now.ToString();
        }

        private void LoadEvents(int topNRecords)
        {
            this.button1.Enabled = false;
            try
            {
                var topNEntries = _context.FastagEntries
                                .OrderByDescending(x=> x.LastModifiedUtc)
                                .Take(topNRecords)
                                .Select(x=> new Designa.UDP.ReportGenerator.DTO.EventLog()
                                {
                                    EventDate= x.EntryTimeConverted,
                                    FastagEvent= x.EntryResponseStatusDesc,
                                    LaneId = x.EntryLaneId

                                })
                                .ToList();

                var topNPayments = _context.FastagPayments
                                .OrderByDescending(x => x.LastModifiedUtc)
                                .Take(topNRecords)
                                .Select(x => new Designa.UDP.ReportGenerator.DTO.EventLog()
                                {
                                    EventDate = x.ExitTimeConverted,
                                    FastagEvent = x.FastagResponseStatusDesc,
                                    LaneId = x.ExitLaneId

                                })
                                .ToList();

                topNEntries.AddRange(topNPayments);

                var resultList = topNEntries.OrderByDescending(x=> x.EventDate).Take(topNRecords).ToList();

                var bindingList = new BindingList<Designa.UDP.ReportGenerator.DTO.EventLog>(resultList);
                var source = new BindingSource(bindingList, null);

                dataGridView1.DataSource = bindingList;


            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.button1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var count = _configuration["LiveEventsFeedRowCount"] != null ? Convert.ToInt32(_configuration["LiveEventsFeedRowCount"]) : 100;
            LoadEvents(count);
            label1.Text = "Last Refreshed at "+ DateTime.Now.ToString();
        }
    }
}
