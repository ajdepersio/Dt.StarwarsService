using Dt.StarwarsService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Client
{
    internal interface ISwapiClient
    {
        IStarshipService Starships { get; }
    }
}
