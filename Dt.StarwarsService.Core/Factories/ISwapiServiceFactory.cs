using Dt.StarwarsService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Factories
{
    /// <summary>
    /// Provides factory methods for creating different services
    /// </summary>
    public interface ISwapiServiceFactory
    {
        /// <summary>
        /// Creates the Starship service
        ///     <seealso cref="IStarshipService"/>
        /// </summary>
        /// <returns></returns>
        IStarshipService CreateStarshipService();
    }
}
