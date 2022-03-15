using Dt.StarwarsService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Repositories
{
    /// <summary>
    /// Provides low-level access to the SWAPI functionality
    /// </summary>
    public interface ISwapiRepository
    {
        /// <summary>
        /// Retrieves all starships
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Starship>> GetStarships();
    }
}
