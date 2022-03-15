using Dt.StarwarsService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Factories
{
    public interface ISwapiServiceFactory
    {
        IStarshipService CreateStarshipService();
    }
}
