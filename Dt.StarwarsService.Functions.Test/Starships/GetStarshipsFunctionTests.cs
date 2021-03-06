using Xunit;
using Dt.StarwarsService.Functions.Starships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Dt.StarwarsService.Core.Client;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Net;
using Dt.StarwarsService.Functions.Test.Helpers;
using Dt.StarwarsService.Core.Services;
using Dt.StarwarsService.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dt.StarwarsService.Functions.Starships.Test
{
    public class GetStarshipsFunctionTests
    {
        [Fact]
        public async void RunTest()
        {
            var swapiClient = MockHelpers.CreateSwapiClientMock(true);
            var logger = new Mock<ILogger<GetStarshipsFunction>>();

            var getStarshipFunction = new GetStarshipsFunction(swapiClient.Object, logger.Object);
            var request = new Mock<HttpRequest>();

            var response = await getStarshipFunction.Run(request.Object).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.GetHttpStatusCode());
        }

        [Fact]
        public async void RunTest_Failure()
        {
            var swapiClient = MockHelpers.CreateSwapiClientMock(false);
            var logger = new Mock<ILogger<GetStarshipsFunction>>();

            var getStarshipFunction = new GetStarshipsFunction(swapiClient.Object, logger.Object);
            var request = new Mock<HttpRequest>();

            var response = await getStarshipFunction.Run(request.Object).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.GetHttpStatusCode());
        }
    }
}