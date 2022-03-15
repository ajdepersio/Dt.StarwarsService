using Xunit;
using Dt.StarwarsService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dt.StarwarsService.Core.Repositories;
using Moq;
using Dt.StarwarsService.Core.Entities;

namespace Dt.StarwarsService.Core.Services.Tests
{
    public class StarshipServiceTests
    {
        [Fact]
        public async void GetAllTest()
        {
            var repository = new Mock<ISwapiRepository>();
            var ships = new List<Starship>()
                {
                    new Starship()
                    {
                        Name = "The Biggin'",
                        Length = "10000000000000"
                    },
                    new Starship()
                    {
                        Name = "The Biggin' Jr.",
                        Length = "1000000"
                    }
                };

            var getStarshipsResult = new Mock<IEnumerable<Starship>>();
            getStarshipsResult.Setup(m => m.GetEnumerator()).Returns(() => ships.GetEnumerator());

            repository.Setup(x => x.GetStarships())
                .Returns(Task.FromResult(getStarshipsResult.Object));

            var service = new StarshipService(repository.Object);
            var results = await service.GetAll().ConfigureAwait(false);
            
            Assert.True(results.Success);
            Assert.Equal(2, results.Starships?.Count());
            Assert.Equal("The Biggin'", results.Starships?.First().Name);
            Assert.Null(results.Exception);
        }

        [Fact]
        public async void GetAllTest_Failure()
        {
            var repository = new Mock<ISwapiRepository>();

            repository.Setup(x => x.GetStarships())
                .Throws(new Exception("It Borked!"));

            var service = new StarshipService(repository.Object);
            var results = await service.GetAll().ConfigureAwait(false);

            Assert.False(results.Success);
            Assert.Null(results.Starships);
            Assert.NotNull(results.Exception);
        }
    }
}