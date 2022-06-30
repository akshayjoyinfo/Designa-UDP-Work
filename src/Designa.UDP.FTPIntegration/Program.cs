using Designa.UDP.Reciever.Service.Application.Services;
using Designa.UDP.Reciever.Service.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace Designa.UDP.FTPIntegration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fastag - FTP Ingeration Services Started");
            IConfiguration Configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .AddJsonFile(path: "FtpConfig.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        .CreateLogger();

            var services = new ServiceCollection();
            services.AddDbContext<UDPDbContext>(options =>
            {
                var connString = Configuration.GetConnectionString("UDPDBConnection");
                var decryptedConnString = StringCipher.Decrypt(connString);
                options.UseNpgsql(decryptedConnString).UseSnakeCaseNamingConvention();
                // options.EnableSensitiveDataLogging(); -- enable when u really want to see the Ef query generate logs
            });

            new FtpService(Configuration, services).CreateFtpReports();
        }
    }
}
