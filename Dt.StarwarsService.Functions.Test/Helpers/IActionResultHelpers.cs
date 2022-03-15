using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Functions.Test.Helpers
{
    internal static class IActionResultHelpers
    {
        public static HttpStatusCode GetHttpStatusCode(this IActionResult functionResult)
        {
            return (HttpStatusCode)functionResult
                .GetType()
                .GetProperty("StatusCode")
                .GetValue(functionResult, null);
        }
    }
}
