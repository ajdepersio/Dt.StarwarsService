using Dt.StarwarsService.Core.Client;
using Dt.StarwarsService.Core.Entities;
using Dt.StarwarsService.Core.Extensions;
using Dt.StarwarsService.Core.Models;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Repositories
{
    internal class SwapiRepository : ISwapiRepository
    {
        private readonly SwapiSettings _settings;
        private readonly ILogger<ISwapiRepository> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _starshipsRoute;

        public SwapiRepository(SwapiSettings settings, ILogger<ISwapiRepository> logger)
        {
            _settings = settings;
            _logger = logger;
            _httpClient = new HttpClient();
            _starshipsRoute = $"{_settings.BaseUrl}/starships";
        }

        private AsyncRetryPolicy<HttpResponseMessage> RetryPolicy => Policy
            .HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .RetryAsync(_settings.MaxRetries, (_, __) => _logger.LogWarning("Retrying Request"));

        public async Task<IEnumerable<Starship>> GetStarships()
        {
            var url = _starshipsRoute;
            var pageResponse = new PagedResponseModel<Starship>();

            var results = new List<Starship>();
            do
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await this.MakeRequest(request).ConfigureAwait(false);

                await using var stream = await GetResponseBody(response).ConfigureAwait(false);

                pageResponse = await PagedResponseModel<Starship>.FromJson(stream).ConfigureAwait(false);

                if (pageResponse.Results != null)
                {
                    results.AddRange(pageResponse.Results);
                }

                if (pageResponse.Next != null)
                {
                    url = pageResponse.Next.ToString();
                }
            } while (pageResponse.Next != null);

            return results;
        }

        private async Task<HttpResponseMessage> MakeRequest(HttpRequestMessage request)
        {
            var response = new HttpResponseMessage();

            _logger.LogInformation($"HTTP Request: {request.RequestUri}");
            response = await this.RetryPolicy.ExecuteAsync(async () =>
            {
                request = request.Clone();
                return await _httpClient.SendAsync(request).ConfigureAwait(false);
            }).ConfigureAwait(false);

            _logger.LogInformation($"HTTP Response: {response.StatusCode}");
            response.EnsureSuccessStatusCode();

            return response;
        }

        private static async Task<Stream> GetResponseBody(HttpResponseMessage response)
        {
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return stream;
        }
    }
}
