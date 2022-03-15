using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Models
{
    internal class PagedResponseModel<T>
    {
        public int Count { get; set; }
        public Uri? Next { get; set; }
        public Uri? Previous { get; set; }
        public IEnumerable<T>? Results { get; set; }

        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<PagedResponseModel<T>> FromJson(Stream jsonStream)
        {
            if (!jsonStream.CanRead)
            {
                throw new AccessViolationException("Cannot read jsonStream");
            }

            var response = await JsonSerializer.DeserializeAsync<PagedResponseModel<T>>(jsonStream, _serializerOptions).ConfigureAwait(false);
            return response;
        }
    }
}
