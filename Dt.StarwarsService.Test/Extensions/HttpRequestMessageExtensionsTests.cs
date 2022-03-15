using Xunit;
using Dt.StarwarsService.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Dt.StarwarsService.Core.Extensions.Tests
{
    public class HttpRequestMessageExtensionsTests
    {
        [Fact]
        public void CloneTest()
        {

            var request = new HttpRequestMessage(HttpMethod.Post, "https://andrew.depersio.net");

            var clonedRequest = request.Clone();

            Assert.Equal(request.RequestUri, clonedRequest.RequestUri);
            Assert.Equal(request.Method, clonedRequest.Method);
        }
    }
}