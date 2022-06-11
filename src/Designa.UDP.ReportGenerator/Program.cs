using Designa.UDP.Reciever.Service.Application.Services;
using Designa.UDP.Reciever.Service.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Designa.UDP.ReportGenerator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .AddJsonFile(path: "Config.json", optional: false, reloadOnChange: true)
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

                //ServiceProvider serviceProvider = services.BuildServiceProvider();

                //var dbContext = serviceProvider.GetService<UDPDbContext>();
                //dbContext.Database.Migrate();

                Application.Run(new MainWindow(Configuration,services));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Starting up Designa.UDP.Reciever.Service ... " + ex.ToString());
                throw ex;
            }
            
        }
    }
}
