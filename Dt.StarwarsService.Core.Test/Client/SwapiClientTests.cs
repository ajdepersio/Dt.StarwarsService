using Xunit;
using Dt.StarwarsService.Core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dt.StarwarsService.Core.Factories;
using Dt.StarwarsService.Core.Repositories;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Dt.StarwarsService.Core.Client.Tests
{
    public class SwapiClientTests
    {
        private static readonly SwapiSettings _settings = new SwapiSettings()
        {
            BaseUrl = "https://swapi.dev/api/",
            MaxRetries = 5
        };

        [Fact]
        public void SwapiClientTest()
        {
            var client = new SwapiClient(new SwapiServiceFactory(new SwapiRepository(_settings)));

            Assert.NotNull(client);
            Assert.NotNull(client.Starships);
        }

        [Fact]
        public void SwapiClientTest_Failure()
        {
            var factory = new Mock<ISwapiServiceFactory>();
            factory.Setup(x => x.CreateStarshipService())
                .Throws(new Exception("It borked!"));

            var exception = Record.Exception(() => new SwapiClient(factory.Object));

            Assert.NotNull(exception);
            Assert.Equal("It borked!", exception.Message);
        }
    }
}