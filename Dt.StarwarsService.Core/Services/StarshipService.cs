using Dt.StarwarsService.Core.Entities;
using Dt.StarwarsService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Services
{
    /// <inheritdoc cref="IStarshipService"/>
    internal class StarshipService : IStarshipService
    {
        private readonly ISwapiRepository _repository;

        public StarshipService(ISwapiRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool Success, IEnumerable<Starship>? Starships, Exception? Exception)> GetAll()
        {
            try
            {
                var starships = await _repository.GetStarships().ConfigureAwait(false);
                return (true, starships, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex);
            }
        }
    }
}
