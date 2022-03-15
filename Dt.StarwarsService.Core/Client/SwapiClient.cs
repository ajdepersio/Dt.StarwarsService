using Dt.StarwarsService.Core.Factories;
using Dt.StarwarsService.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Client
{
    public class SwapiClient : ISwapiClient
    {
        private readonly ILogger<ISwapiClient> _logger;

        public IStarshipService Starships { get; }

        public SwapiClient(ISwapiServiceFactory factory, ILogger<ISwapiClient>? logger = default)
        {
            _logger = logger ?? new NullLoggerFactory().CreateLogger<ISwapiClient>();

            try
            {
                this.Starships = factory.CreateStarshipService();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            _logger.LogInformation("SwapiClient Created");
        }
    }
}
