using Designa.UDP.Reciever.Service.Application.DTO;
using Designa.UDP.Reciever.Service.Application.Shared;
using Designa.UDP.Reciever.Service.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Designa.UDP.Reciever.Service.Application.Services
{
    public class LedService
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Serilog.ILogger log = Log.Logger;
        private readonly ServiceProvider _serviceProvider;
        private readonly UDPDbContext _context;
        public LedService(IConfiguration configuration, ServiceCollection services)
        {
            _configuration = configuration;
            _services = services;

            _serviceProvider = services.BuildServiceProvider();
            _context = _serviceProvider.GetService<UDPDbContext>();
        }

        public void ShowDisplay(FastagEntryMessage entry)
        {
            
            var laneConfig = _context.LaneConfigurations.FirstOrDefault(x => x.LaneId == entry.FastagEntry.EntryLaneId);

            if (entry.FastagEntryRes != null)
            {
                var responseCode = entry.FastagEntryRes?.Details.FirstOrDefault()?.StatusCode.ToString();


                if (responseCode == "200" || responseCode == "305" || responseCode == "307" || responseCode == "206" || responseCode == "400")
                {
                    return;
                }
                else if (responseCode == "203" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, entry?.FastagEntry.TicketId,"red");
                }
                else if (responseCode == "202" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG ENTERED", laneConfig.Ip, entry?.FastagEntry.TicketId, "green");
                }
                else if (responseCode == "204" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
                }
                else if (responseCode == "205" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
                }
                else if (responseCode == "301" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
                }
                else if (responseCode == "310" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
                }
                else if (responseCode == "404" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
                }
                else if (responseCode == "600" && laneConfig != null)
                {
                    SendDisplaytext("TAG NOT WORKING", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
                }
                else if (responseCode == "602" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
                }
                else
                {
                    if (laneConfig?.Ip != null)
                    {
                        SendDisplaytext("FASTAG NOT PROCESS", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
                    }
                }
            }
            else
            {
                SendDisplaytext("FASTAG OFFLINE", laneConfig.Ip, entry?.FastagEntry.TicketId, "red");
            }

            ResetDisplaytext(laneConfig?.DisplayResetMessage != null ? laneConfig.DisplayResetMessage : "", laneConfig.Ip, "white");
        }
        public void ShowDisplay(FastagPaymentMessage payment)
        {

            var laneConfig = _context.LaneConfigurations.FirstOrDefault(x => x.LaneId == payment.FastagPayment.ExitLaneId);

            if (payment.FastagPaymentRes != null)
            {
                var responseCode = payment.FastagPaymentRes?.StatusCode.ToString();


                if (responseCode == "203")
                {
                    return;
                }
                else if (responseCode == "200" && laneConfig != null)
                {
                    SendDisplaytext($"FASTAG FEE Rs. {Math.Round(payment.FastagPayment.TxnAmount,2)}", laneConfig.Ip, payment.FastagPayment.TicketId, "green");
                }
                else if (responseCode == "202" && laneConfig != null)
                {
                    SendDisplaytext($"FASTAG FEE Rs. {Math.Round(payment.FastagPayment.TxnAmount,2)}", laneConfig.Ip, payment.FastagPayment.TicketId, "green");
                }
                else if (responseCode == "204" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, payment.FastagPayment.TicketId, "red");
                }
                else if (responseCode == "205" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, payment.FastagPayment.TicketId, "red");
                }
                else if (responseCode == "301" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, payment.FastagPayment.TicketId, "red");
                }
                else if((responseCode == "206" || responseCode == "310" || responseCode == "404" || responseCode == "602") && laneConfig != null)
                {
                    SendDisplaytext("FASTAG BLACKLISTED", laneConfig.Ip, payment.FastagPayment.TicketId, "red");
                }
                else if (responseCode == "305" && laneConfig != null)
                {
                    SendDisplaytext("15MIN, TXN DENIED", laneConfig.Ip, payment.FastagPayment.TicketId, "red");
                }
                else if (responseCode == "307" && laneConfig != null)
                {
                    SendDisplaytext(">1500, TXN DENIED", laneConfig.Ip, payment.FastagPayment.TicketId, "red");
                }
                else if (responseCode == "400" && laneConfig != null)
                {
                    SendDisplaytext("FASTAG TXN FAILED", laneConfig.Ip, payment.FastagPayment.TicketId, "red");
                }
                else if (responseCode == "600" && laneConfig != null)
                {
                    SendDisplaytext("TAG NOT WORKING", laneConfig.Ip, payment.FastagPayment.TicketId, "red");
                }
                else
                {
                    if (laneConfig?.Ip != null)
                    {
                        SendDisplaytext("FASTAG NOT PROCESS", laneConfig.Ip, payment?.FastagPayment.TicketId, "red");
                    }
                }
            }
            else
            {
                SendDisplaytext("FASTAG OFFLINE", laneConfig.Ip, payment.FastagPayment.TicketId);
            }

            ResetDisplaytext(laneConfig?.DisplayResetMessage!=null?laneConfig.DisplayResetMessage: "", laneConfig.Ip, "white");
        }

        public void SendDisplaytext(string text, string ip, string ticketId, string color="red")
        {
            var IP = Encoding.ASCII.GetBytes(ip);
            var WIDTH = Convert.ToInt32(_configuration["WIDTH"]);
            var HEIGHT = Convert.ToInt32(_configuration["HEIGHT"]);
            var PORT = Convert.ToUInt16(_configuration["PORT"]);
            var TYPE = Convert.ToInt32(_configuration["TYPE"]);
            var USER_NAME = _configuration["USER_NAME"];
            var USER_PASSWORD = _configuration["USER_PASSWORD"];
            var DISPLAY_DELAY= Convert.ToInt32(_configuration["LedDisplayTimeInMilliSeconds"]);
            var FONT_SIZE = Convert.ToInt32(_configuration["FontSize"]);
            var X = Convert.ToInt32(_configuration["LedDisplayXArea"]);
            var Y = Convert.ToInt32(_configuration["LedDisplayYArea"]);
            try
            {
                bool BL = true;

                LedYNetSdk.init_sdk();
                int err = 0;

                //
                IntPtr playlist = LedYNetSdk.create_playlist(WIDTH, HEIGHT, TYPE);
                string name = "program_0";
                IntPtr program = LedYNetSdk.create_program(name, "0xff000000");
                //
                IntPtr area_tree = LedYNetSdk.create_rich_text();
                err = LedYNetSdk.add_rich_text_unit(area_tree, 0, 16, "", "0xff000000", $"<span foreground='{color}' font='{FONT_SIZE}'>" + text + "</span>");
                err = LedYNetSdk.add_rich_text(program, area_tree, X, Y, WIDTH, HEIGHT, 100, 1, 0);
                LedYNetSdk.delete_rich_text(area_tree);

                string m_aging_start_time = "";
                string m_aging_stop_time = "";
                string m_period_ontime = "";
                string m_period_offtime = "";
                err = LedYNetSdk.add_program_in_playlist(playlist, program, 1, 10, m_aging_start_time, m_aging_stop_time, m_period_ontime, m_period_offtime, 127);

                int send_style = 0;
                var szLocalTempDir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                long free_size = 0; long total_size = 0;
                err = LedYNetSdk.send_program(IP, PORT, USER_NAME, USER_PASSWORD, szLocalTempDir, playlist, send_style, ref free_size, ref total_size);

                if (err == 0)
                {
                    log.Information($"Display Success {ticketId}: Message:{text} Color: {color} ");
                }
                else
                {
                    log.Information($"Display Error {ticketId}: Message:{text} Color: {color} {Error.GetError(err)}");
                }
                Thread.Sleep(DISPLAY_DELAY);
               // LedYNetSdk.delete_playlist(playlist);
            }
            catch (AccessViolationException ee)
            {
                log.Error($"LEDSDK Error {ee.ToString()}");
            }
            catch (Exception exp) { Console.Write(exp); }
            finally
            {
                

                //LedYNetSdk.clear_all_program(IP, (ushort)PORT, USER_NAME, USER_PASSWORD);
                ////清除显示所有动态区
                //LedYNetSdk.clear_dynamic(IP, (ushort)PORT, USER_NAME, USER_PASSWORD);



                LedYNetSdk.release_sdk();
            }
        }

        public void ResetDisplaytext(string text, string ip, string color = "white")
        {
            var IP = Encoding.ASCII.GetBytes(ip);
            var WIDTH = Convert.ToInt32(_configuration["WIDTH"]);
            var HEIGHT = Convert.ToInt32(_configuration["HEIGHT"]);
            var PORT = Convert.ToUInt16(_configuration["PORT"]);
            var TYPE = Convert.ToInt32(_configuration["TYPE"]);
            var USER_NAME = _configuration["USER_NAME"];
            var USER_PASSWORD = _configuration["USER_PASSWORD"];
            var DISPLAY_DELAY = Convert.ToInt32(_configuration["LedDisplayTimeInMilliSeconds"]);
            var FONT_SIZE = Convert.ToInt32(_configuration["FontSize"]);
            var X = Convert.ToInt32(_configuration["LedDisplayXArea"]);
            var Y = Convert.ToInt32(_configuration["LedDisplayYArea"]);

            try
            {
                bool BL = true;

                LedYNetSdk.init_sdk();
                int err = 0;

                //
                IntPtr playlist = LedYNetSdk.create_playlist(WIDTH, HEIGHT, TYPE);
                string name = "program_clear";
                IntPtr program = LedYNetSdk.create_program(name, "0xff000000");
                //
                IntPtr area_tree = LedYNetSdk.create_rich_text();
                err = LedYNetSdk.add_rich_text_unit(area_tree, 0, 16, "", "0xff000000", $"<span foreground='{color}' font='{FONT_SIZE}'>" + text + "</span>");
                err = LedYNetSdk.add_rich_text(program, area_tree, X, Y, WIDTH, HEIGHT, 100, 1, 0);
                LedYNetSdk.delete_rich_text(area_tree);

                string m_aging_start_time = "";
                string m_aging_stop_time = "";
                string m_period_ontime = "";
                string m_period_offtime = "";
                err = LedYNetSdk.add_program_in_playlist(playlist, program, 1, 10, m_aging_start_time, m_aging_stop_time, m_period_ontime, m_period_offtime, 127);

                int send_style = 0;
                var szLocalTempDir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                long free_size = 0; long total_size = 0;
                err = LedYNetSdk.send_program(IP, PORT, USER_NAME, USER_PASSWORD, szLocalTempDir, playlist, send_style, ref free_size, ref total_size);

                if (err == 0)
                {
                    log.Information($"Display Reset DONE");
                }
                else
                {
                    log.Information($"Display Reset Error {Error.GetError(err)}");
                }
                
                // LedYNetSdk.delete_playlist(playlist);
            }
            catch (AccessViolationException ee)
            {
                log.Information($"LEDSDK Error {ee.ToString()}");
            }
            catch (Exception exp) { Console.Write(exp); }
            finally
            {


                //LedYNetSdk.clear_all_program(IP, (ushort)PORT, USER_NAME, USER_PASSWORD);
                ////清除显示所有动态区
                //LedYNetSdk.clear_dynamic(IP, (ushort)PORT, USER_NAME, USER_PASSWORD);



                LedYNetSdk.release_sdk();
            }
        }

    }
}
