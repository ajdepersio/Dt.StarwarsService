using Dt.StarwarsService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Repositories
{
    internal interface ISwapiRepository
    {
        Task<IEnumerable<Starship>> GetStarships();
    }
}
