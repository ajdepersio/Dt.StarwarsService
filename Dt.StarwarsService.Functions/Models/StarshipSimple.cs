using Dt.StarwarsService.Core.Entities;
using System.Collections.Generic;

namespace Dt.StarwarsService.Functions.Models
{
    public class StarshipSimple
    {
        public StarshipSimple(Starship starship)
        {
            this.Name = starship.Name;
            this.StarshipClass = starship.StarshipClass;
            this.CargoCapacity = starship.CargoCapacity;
            this.Manufacturers = starship.Manufacturer.Split(", ");
        }

        public string Name { get; set; }
        public string StarshipClass { get; set; }
        public string CargoCapacity { get; set; }
        public IEnumerable<string> Manufacturers { get; set; }
    }
}