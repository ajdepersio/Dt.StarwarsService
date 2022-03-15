﻿using Dt.StarwarsService.Core.Repositories;
using Dt.StarwarsService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Factories
{
    internal class SwapiServiceFactory : ISwapiServiceFactory
    {
        private readonly ISwapiRepository _repository;

        public IStarshipService CreateStarshipService()
        {
            return new StarshipService(_repository);
        }
    }
}
