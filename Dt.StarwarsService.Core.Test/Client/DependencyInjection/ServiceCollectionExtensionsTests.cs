using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dt.StarwarsService.Core.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Dt.StarwarsService.Core.Client.DependencyInjection.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddSwapiClientTest()
        {
            var services = new ServiceCollection();
            services.AddSwapiClient(new SwapiSettings());

            var serviceProvider = services.BuildServiceProvider();
            var swapiClient = serviceProvider.GetRequiredService<ISwapiClient>();

            Assert.NotNull(swapiClient);
            Assert.NotNull(swapiClient.Starships);
        }
    }
}