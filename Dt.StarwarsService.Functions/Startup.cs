using Dt.StarwarsService.Core.Client;
using Dt.StarwarsService.Core.Client.DependencyInjection;
using Dt.StarwarsService.Core.Factories;
using Dt.StarwarsService.Core.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Dt.StarwarsService.Functions.Startup))]

namespace Dt.StarwarsService.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();
            var swapiSettings = new SwapiSettings();
            builder.GetContext().Configuration.GetSection("SwapiSettings").Bind(swapiSettings);

            builder.Services.AddSwapiClient(swapiSettings);
        }
    }
}