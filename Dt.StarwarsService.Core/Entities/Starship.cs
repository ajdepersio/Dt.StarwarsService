using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Entities
{
    public class Starship
    {
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Manufacturer { get; set; }

        [JsonPropertyName("cost_in_credits")]
        public string? CostInCredits { get; set; }

        public string? Length { get; set; }

        [JsonPropertyName("max_atmosphering_speed")]
        public string? MaxAtmospheringSpeed { get; set; }

        public string? Crew { get; set; }
        public string? Passengers { get; set; }

        [JsonPropertyName("cargo_capacity")]
        public string? CargoCapacity { get; set; }

        public string? Consumables { get; set; }

        [JsonPropertyName("hyperdrive_rating")]
        public string? HyperdriveRating { get; set; }

        public string? Mglt { get; set; }

        [JsonPropertyName("starship_class")]
        public string? StarshipClass { get; set; }

        public Uri[]? Pilots { get; set; }
        public Uri[]? Films { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Edited { get; set; }
        public Uri? Url { get; set; }
    }
}
