﻿using Designa.UDP.Reciever.Service.Persistence;
using Designa.UDP.Reciever.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using Topper;

namespace Designa.UDP.Reciever.Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(Configuration)
                            .CreateLogger();

                var services = new ServiceCollection();
                services.AddDbContext<UDPDbContext>(options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString("UDPDBConnection")).UseSnakeCaseNamingConvention();
                    // options.EnableSensitiveDataLogging(); -- enable when u really want to see the Ef query generate logs
                });

                ServiceProvider serviceProvider = services.BuildServiceProvider();

                var dbContext = serviceProvider.GetService<UDPDbContext>();
                dbContext.Database.Migrate();

                var configuration = new ServiceConfiguration()
                    .Add("UDPReciever", () => new UDPReciever(Configuration, services));

                ServiceHost.Run(configuration);

            }
            catch(Exception ex)
            {
                Console.WriteLine("Error while Starting up Designa.UDP.Reciever.Service ... "+ex.ToString());
            }
        }
    }
}
