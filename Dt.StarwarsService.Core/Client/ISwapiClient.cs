﻿using Dt.StarwarsService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Client
{
    public interface ISwapiClient
    {
        IStarshipService Starships { get; }
    }
}
