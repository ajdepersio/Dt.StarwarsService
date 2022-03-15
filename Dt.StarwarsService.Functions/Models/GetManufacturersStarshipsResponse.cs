using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Functions.Models
{
    internal class GetManufacturersStarshipsResponse
    {
        public IEnumerable<StarshipSimple> Starships { get; set; }
    }
}
