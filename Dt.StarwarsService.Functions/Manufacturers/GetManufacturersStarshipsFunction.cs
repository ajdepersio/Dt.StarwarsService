using System;
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
    public class GetManufacturersStarshipsFunction
    {
        private readonly ISwapiClient _swapiClient;
        private readonly ILogger<GetManufacturersStarshipsFunction> _logger;

        public GetManufacturersStarshipsFunction(ISwapiClient swapiClient, ILogger<GetManufacturersStarshipsFunction> log)
        {
            _swapiClient = swapiClient;
            _logger = log;
        }

        [FunctionName("GetManufacturersStarshipsFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Manufacturer" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The Manufacturer Name")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetManufacturersStarshipsResponse), Description = "List of all starships by the given manufacturer.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Something went wrong.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "manufacturer/{name}/starships")] HttpRequest req, string name)
        {
            try
            {
                _logger.LogInformation($"Received Request for GetManufacturersStarshipsFunction [name: {name}]");

                var starshipsResults = await _swapiClient.Starships.GetAll().ConfigureAwait(false);

                if (!starshipsResults.Success)
                {
                    _logger.LogError(starshipsResults.Exception, starshipsResults.Exception.Message);
                    return new InternalServerErrorResult();
                }

                _logger.LogInformation($"Successfully Processed Request for GetManufacturersStarshipsFunction [name: {name}]");

                var starships = starshipsResults.Starships
                    .Select(x => new StarshipSimple(x))
                    .Where(x => x.Manufacturers.Contains(name))
                    .ToList();

                if (starships.Count == 0)
                {
                    return new NotFoundResult();
                }    

                return new OkObjectResult(new GetManufacturersStarshipsResponse() { Starships = starships });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new InternalServerErrorResult();
            }
        }
    }
}

