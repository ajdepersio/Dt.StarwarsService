using Dt.StarwarsService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Services
{
    /// <summary>
    /// Provides access to starship related functionality
    /// </summary>
    public interface IStarshipService
    {
        /// <summary>
        /// Retrieves all starships
        /// </summary>
        Task<(bool Success, IEnumerable<Starship>? Starships, Exception? Exception)> GetAll();
    }
}
