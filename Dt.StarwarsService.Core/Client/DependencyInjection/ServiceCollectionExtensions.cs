using Dt.StarwarsService.Core.Client;
using Dt.StarwarsService.Core.Factories;
using Dt.StarwarsService.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Client.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the SwapiClient to the DI Service Collection.
        ///     <seealso cref="ISwapiClient"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwapiClient(this IServiceCollection services, SwapiSettings settings)
        {
            services.AddSingleton(settings);
            services.AddScoped<ISwapiRepository, SwapiRepository>();
            services.AddScoped<ISwapiServiceFactory, SwapiServiceFactory>();
            services.AddScoped<ISwapiClient, SwapiClient>();

            return services;
        }
    }
}
