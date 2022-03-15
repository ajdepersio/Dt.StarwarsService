using Xunit;
using Dt.StarwarsService.Functions.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Dt.StarwarsService.Core.Services;
using Dt.StarwarsService.Core.Entities;
using Dt.StarwarsService.Core.Client;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Dt.StarwarsService.Functions.Test.Helpers;
using System.Net;

namespace Dt.StarwarsService.Functions.Manufacturers.Test
{
    public class GetManufacturersFunctionTests
    {
        [Fact]
        public async void RunTest()
        {
            var swapiClient = MockHelpers.CreateSwapiClientMock(true);
            var logger = new Mock<ILogger<GetManufacturersFunction>>();

            var getManufacturersFunction = new GetManufacturersFunction(swapiClient.Object, logger.Object);
            var request = new Mock<HttpRequest>();

            var response = await getManufacturersFunction.Run(request.Object).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.GetHttpStatusCode());
        }

        [Fact]
        public async void RunTest_Failure()
        {
            var swapiClient = MockHelpers.CreateSwapiClientMock(false);
            var logger = new Mock<ILogger<GetManufacturersFunction>>();

            var getManufacturersFunction = new GetManufacturersFunction(swapiClient.Object, logger.Object);
            var request = new Mock<HttpRequest>();

            var response = await getManufacturersFunction.Run(request.Object).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.GetHttpStatusCode());
        }
    }
}