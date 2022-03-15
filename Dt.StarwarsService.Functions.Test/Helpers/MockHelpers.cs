using Dt.StarwarsService.Core.Client;
using Dt.StarwarsService.Core.Entities;
using Dt.StarwarsService.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Functions.Test.Helpers
{
    internal static class MockHelpers
    {
        internal static Mock<ISwapiClient> CreateSwapiClientMock(bool getStarshipsSuccess)
        {
            (bool Success, IEnumerable<Starship>? Starships, Exception? Exception) results = new(false, null, new Exception("It borked!"));

            if (getStarshipsSuccess)
            {
                var ships = new List<Starship>()
                    {
                        new Starship()
                        {
                            Name = "The Biggin'",
                            StarshipClass = "Big",
                            CargoCapacity = "a lot",
                            Manufacturer = "Jim Bob's Big Ships"
                        },
                        new Starship()
                        {
                            Name = "The Biggin' Jr.",
                            StarshipClass = "Kinda Big",
                            CargoCapacity = "some",
                            Manufacturer = "Jim Bob's Big Ships, Billy Bob's Kinda Big Ships"
                        }
                    };

                results = new(true, ships, null);
            }

            var starshipsService = new Mock<IStarshipService>();
            starshipsService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(results));

            var swapiClient = new Mock<ISwapiClient>();
            swapiClient.SetupGet(x => x.Starships).Returns(starshipsService.Object);
            return swapiClient;
        }
    }
}
