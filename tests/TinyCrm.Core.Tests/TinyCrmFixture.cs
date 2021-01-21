using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TinyCrm.Core.Services.Extensions;

namespace TinyCrm.Core.Tests
{
    public class TinyCrmFixture : IDisposable
    {
        public IServiceScope Scope { get; private set; }

        public TinyCrmFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Initialize Dependency container
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAppServices(config);

            Scope = serviceCollection
                .BuildServiceProvider()
                .CreateScope();
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}
