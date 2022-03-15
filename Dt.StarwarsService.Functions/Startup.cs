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
            var config = new ConfigurationBuilder()
                // This gives you access to your application settings 
                // in your local development environment
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                // This is what actually gets you the 
                // application settings in Azure
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddLogging();
            var swapiSettings = new SwapiSettings();
            config.GetSection("SwapiSettings").Bind(swapiSettings);

            builder.Services.AddSwapiClient(swapiSettings);

            //builder.Services.AddHttpClient();

            //builder.Services.AddSingleton<IMyService>((s) => {
            //    return new MyService();
            //});

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}