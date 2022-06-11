using Designa.UDP.Reciever.Service.Persistence;
using Designa.UDP.Reciever.Service.Persistence.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Designa.UDP.ReportGenerator
{
    public partial class frmLaneConfiguration : Form
    {
        private readonly Serilog.ILogger log = Log.Logger;
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly ServiceProvider _serviceProvider;
        private readonly UDPDbContext _context;


        public frmLaneConfiguration(IConfiguration configuration, ServiceCollection services)
        {
            InitializeComponent();
            _configuration = configuration;
            _services = services;
            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetService<UDPDbContext>();
            LoadLaneConfigurations();
        }

        private void LoadLaneConfigurations()
        {
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            try
            {
                var laneConfigurations = _context.LaneConfigurations
                                .OrderByDescending(x => x.LastModifiedUtc)
                                .ToList();

                laneConfigurationBindingSource.DataSource = laneConfigurations;
                dataGridView1.DataSource = laneConfigurationBindingSource;
                dataGridView1.EndEdit();

            }
            catch (Exception ex)
            {
                log.Information("Error while fetching LaneConfiguration {ex}", ex);
                MessageBox.Show("Error ocrrued while fetching LaneConfigurations ", ex.Message);
            }
            finally
            {
                this.button1.Enabled = true;
                this.button2.Enabled = false;
                this.button3.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

            }
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            DataGridView sndr = (DataGridView)sender;

            if (sndr.Rows.Count == 1) // <-- if there are no rows in the DataGridView when it paints, then it will create your message
            {
                using (Graphics grfx = e.Graphics)
                {
                    // create a white rectangle so text will be easily readable
                    grfx.FillRectangle(Brushes.White, new Rectangle(new Point(), new Size(sndr.Width - 2, 50)));
                    // write text on top of the white rectangle just created
                    grfx.DrawString("No Lane Configurations Availlable", new Font("Arial", 16), Brushes.Black, new PointF(3, 3));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var laneId = textBox1.Text;
                var ip = textBox2.Text;
                var message = textBox3.Text;

                var laneConfig = new LaneConfiguration()
                {
                    LaneId = laneId,
                    Ip = ip,
                    DisplayResetMessage = message,  
                    LastModifiedUtc = DateTime.Now,
                };

                _context.LaneConfigurations.Add(laneConfig);
                _context.SaveChanges();
                LoadLaneConfigurations(); 
            }
            catch (Exception ex)
            {
                log.Information("Error while Adding LaneConfiguration {ex}", ex);
                MessageBox.Show("Error ocrrued while Adding LaneConfigurations ", ex.Message);
            }
            finally
            {

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView sndr = (DataGridView)sender;
            if (sndr.SelectedRows.Count>=1)
            {
                var row = sndr.SelectedRows[0];
                textBox1.Text = row.Cells[1]?.Value?.ToString();
                textBox2.Text = row.Cells[2]?.Value?.ToString();
                textBox3.Text = row.Cells[3]?.Value?.ToString();
                button2.Enabled = true;
                button3.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count >= 1)
            {
                var row = dataGridView1.SelectedRows[0];
                var laneConfigId= row.Cells[0]?.Value?.ToString();
                var laneConfigIdNumber = Convert.ToInt32(laneConfigId);

                var laneConfig = _context.LaneConfigurations.FirstOrDefault(x => x.Id == laneConfigIdNumber);
                if(laneConfig !=null)
                {
                    laneConfig.LaneId = textBox1.Text;
                    laneConfig.Ip = textBox2.Text;
                    laneConfig.DisplayResetMessage = textBox3.Text;
                    _context.Update(laneConfig);
                    _context.SaveChanges();
                    LoadLaneConfigurations();

                }
            }
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >= 1)
            {
                var row = dataGridView1.SelectedRows[0];
                var laneConfigId = row.Cells[0]?.Value?.ToString();
                var laneConfigIdNumber = Convert.ToInt32(laneConfigId);

                var laneConfig = _context.LaneConfigurations.FirstOrDefault(x => x.Id == laneConfigIdNumber);
                if (laneConfig != null)
                {
                    laneConfig.LaneId = textBox1.Text;
                    laneConfig.Ip = textBox2.Text;
                    laneConfig.DisplayResetMessage = textBox3.Text;

                    _context.Remove(laneConfig);
                    _context.SaveChanges();
                    LoadLaneConfigurations();

                }
            }
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
