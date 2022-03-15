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
#pragma warning disable CS8605 // Unboxing a possibly null value.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return (HttpStatusCode)functionResult
                .GetType()
                .GetProperty("StatusCode")
                .GetValue(functionResult, null);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8605 // Unboxing a possibly null value.
        }
    }
}
