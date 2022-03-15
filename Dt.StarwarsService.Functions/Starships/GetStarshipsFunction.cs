using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Dt.StarwarsService.Core.Client;
using Dt.StarwarsService.Core.Entities;
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
using System.Linq;

namespace Dt.StarwarsService.Functions.Starships
{
    public class GetStarshipsFunction
    {
        private readonly ISwapiClient _swapiClient;
        private readonly ILogger<GetStarshipsFunction> _logger;

        public GetStarshipsFunction(ISwapiClient swapiClient, ILogger<GetStarshipsFunction> log)
        {
            _swapiClient = swapiClient;
            _logger = log;
        }

        [FunctionName("GetStarshipsFunction")]
        [OpenApiOperation(operationId: "GetStarshipsFunction", tags: new[] { "Starship" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetStarshipsResponse), Description = "List of all Starships")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Something went wrong.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "starships")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("Received Request for GetStarshipsFunction");

                var starshipsResults = await _swapiClient.Starships.GetAll().ConfigureAwait(false);

                if (!starshipsResults.Success)
                {
                    _logger.LogError(starshipsResults.Exception, starshipsResults.Exception.Message);
                    return new InternalServerErrorResult();
                }

                _logger.LogInformation("Successfully Processed Request for GetStarshipsFunction");

                var starships = starshipsResults.Starships
                    .Select(x => new StarshipSimple(x))
                    .ToList();

                return new OkObjectResult(new GetStarshipsResponse() { Starships = starships });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new InternalServerErrorResult();
            }
        }
    }
}