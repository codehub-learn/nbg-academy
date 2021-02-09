using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TinyCrm.Core.Data;
using TinyCrm.Core.Config;
using TinyCrm.Core.Config.Extensions;

namespace TinyCrm.Core.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(
            this IServiceCollection @this, IConfiguration configuration)
        {
            @this.AddSingleton<AppConfig>(
                configuration.ReadAppConfiguration());

            // AddScoped
            @this.AddDbContext<CrmDbContext>(
                (serviceProvider, optionsBuilder) => {
                    var appConfig = serviceProvider.GetRequiredService<AppConfig>();

                    optionsBuilder.UseSqlServer(appConfig.CrmConnectionString);
                });

            @this.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
