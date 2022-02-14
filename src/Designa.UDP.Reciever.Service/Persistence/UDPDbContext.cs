using Designa.UDP.Reciever.Service.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Designa.UDP.Reciever.Service.Persistence
{
    public class UDPDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        public DbSet<Config> Configs { get; set; }
        public DbSet<FastagEntryEntity> FastagEntries { get; set; }
        public DbSet<FastagPaymentEntity> FastagPayments { get; set; }
        public DbSet<FastagAuditReport> FastagAuditReports { get; set; }
        public UDPDbContext()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("UDPDBConnection");
        }


        public UDPDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseNpgsql(_connectionString)
                    .UseSnakeCaseNamingConvention();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
