namespace Dt.StarwarsService.Core.Client
{
    public class SwapiSettings
    {
        public int MaxRetries { get; set; }
        public string BaseUrl { get; set; } = "https://swapi.dev/api/";
    }
}