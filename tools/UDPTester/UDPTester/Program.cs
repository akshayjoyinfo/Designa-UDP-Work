using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {

                var builder = new ConfigurationBuilder().AddJsonFile($"Config.json", true, true);

                var config = builder.Build();
                var ipAddress = config["IpAddress"];
                var port = config["Port"];


                Console.WriteLine("Accepted Arguments "+ string.Join("   ", args) + " ,Server Details "+ ipAddress +" : " + port);

                string contents = File.ReadAllText(args[0]);


                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress serverAddr = IPAddress.Parse(ipAddress);
                IPEndPoint endPoint = new IPEndPoint(serverAddr, Convert.ToInt32(port));


                byte[] send_buffer = Encoding.ASCII.GetBytes(contents);
                var res = sock.SendTo(send_buffer, endPoint);
                Console.WriteLine("Send Message " + res);


            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception "+ ex);
            }
            Console.ReadLine();
        }
    }
}
