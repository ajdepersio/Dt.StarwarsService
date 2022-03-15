using Dt.StarwarsService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Client
{
    /// <summary>
    /// Class for accessing various functionality within SWAPI
    /// </summary>
    public interface ISwapiClient
    {
        /// <summary>
        /// Provides access to Starship-related operations
        ///     <seealso cref="IStarshipService"/>
        /// </summary>
        IStarshipService Starships { get; }
    }
}
