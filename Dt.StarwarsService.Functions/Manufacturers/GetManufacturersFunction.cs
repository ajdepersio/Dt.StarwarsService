using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Dt.StarwarsService.Core.Client;
using Dt.StarwarsService.Functions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Dt.StarwarsService.Functions.Manufacturers
{
    public class GetManufacturersFunction
    {
        private readonly ISwapiClient _swapiClient;
        private readonly ILogger<GetManufacturersFunction> _logger;

        public GetManufacturersFunction(ISwapiClient swapiClient, ILogger<GetManufacturersFunction> log)
        {
            _swapiClient = swapiClient;
            _logger = log;
        }

        [FunctionName("GetManufacturersFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Manufacturer" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetManufacturersResponse), Description = "List of all Manufacturers")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Something went wrong.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "manufacturers")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("Received Request for GetManufacturersFunction");

                var starshipsResults = await _swapiClient.Starships.GetAll().ConfigureAwait(false);

                if (!starshipsResults.Success)
                {
                    _logger.LogError(starshipsResults.Exception, starshipsResults.Exception.Message);
                    return new InternalServerErrorResult();
                }

                _logger.LogInformation("Successfully Processed Request for GetManufacturersFunction");

                var uniqueManufacturers = starshipsResults.Starships
                    .Select(x => new StarshipSimple(x))
                    .SelectMany(x => x.Manufacturers)
                    .Distinct()
                    .ToList();

                return new OkObjectResult(uniqueManufacturers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new InternalServerErrorResult();
            }
        }
    }
}

