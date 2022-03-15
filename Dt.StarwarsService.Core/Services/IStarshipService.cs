using Dt.StarwarsService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Services
{
    public interface IStarshipService
    {
        Task<(bool Success, IEnumerable<Starship>? Starships, Exception? Exception)> GetAll();
    }
}
