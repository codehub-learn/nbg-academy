using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using TinyCrm.Core.Data;
using TinyCrm.Core.Config.Extensions;

namespace ConsoleApp2
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CrmDbContext>
    {
        public CrmDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var config = configuration.ReadAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<CrmDbContext>();

            optionsBuilder.UseSqlServer(
                config.CrmConnectionString,
                options => {
                    options.MigrationsAssembly("ConsoleApp2");
                });

            return new CrmDbContext(optionsBuilder.Options);
        }
    }
}
