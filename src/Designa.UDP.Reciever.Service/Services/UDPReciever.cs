using Designa.UDP.Reciever.Service.Application.DTO;
using Designa.UDP.Reciever.Service.Application.Services;
using Designa.UDP.Reciever.Service.Application.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Designa.UDP.Reciever.Service.Services
{
    public class UDPReciever : IDisposable
    {
        private readonly Serilog.ILogger log = Log.Logger;
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly IPAddress _address;

        private readonly UDPMessageService _uDPMessageService;
        private readonly LedService _ledService;
        public UDPReciever(IConfiguration configuration , ServiceCollection services)
        {
            _configuration = configuration;
            _services = services;

            _uDPMessageService = new UDPMessageService(_configuration, _services);
            _ledService = new LedService(_configuration, _services);

            HandleService();
        }

        private void HandleService()
        {
            log.Information("Started UDP Reciever Service Handler");

            var port = Convert.ToInt32(_configuration["UDPPort"]);
            var ipAddress = _configuration["IPAddress"];


            UdpClient listener = new UdpClient(port);
            IPAddress serverAddr = IPAddress.Parse(ipAddress);
            IPEndPoint groupEP = new IPEndPoint(serverAddr, port);

            try
            {
                while (true)
                {
                    log.Debug("Waiting for UDP Message to Recieve");
                    byte[] bytes = listener.Receive(ref groupEP);
                    var message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    log.Information(message);
                    ProcessMessage(message);
                    
                }
            }
            catch (Exception e)
            {
                log.Error("UDPReciever Handler Exception {e}",e);
            }
            finally
            {
                listener.Close();
            }

            log.Information("Started UDP Reciever Service Handler");
        }

        private void ProcessMessage(string message)
        {

            log.Debug("Process Message {message}", message);
            try
            {
                // FastagEntryMessage
                if (message.TryParseJson(out FastagEntryMessage result))
                {
                    if(result!= null && result.FastagEntry.TicketId!= null)
                    {
                        _ledService.ShowDisplay(result);
                        _uDPMessageService.ProcessFastagEntryMessage(result, message);
                    }
                }
                // FastagPaymentMessage
                else if (message.TryParseJson(out FastagPaymentMessage paymentResult))
                {
                    if (paymentResult != null && paymentResult.FastagPayment.TicketId != null)
                    {
                        _ledService.ShowDisplay(paymentResult);
                        _uDPMessageService.ProcessFastagPaymentMessage(paymentResult, message);
                    }
                }


            }
            catch(FormatException fmt)
            {
                log.Error("UDPReciever ProcessMessage exception {fmt}", fmt);
            }
            catch (Exception e)
            {
                log.Error("UDPReciever ProcessMessage exception {e}",e);
            }
        }

        public void Dispose()
        {
            log.Error("UDPReciever Stopped");
        }
    }
}
