namespace Dottor.BrewerApp.Common.Services
{
    using Dottor.BrewerApp.Common.Dtos;
    using Dottor.BrewerApp.Common.Models;
    using Dottor.BrewerApp.Common.Utilities;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// API documentation https://punkapi.com/
    /// </remarks>
    internal class BrewerService : IBrewerService
    {
        public const string BaseUrl = "https://api.punkapi.com";
        public const string HttpClientName = "Brewer";


        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BrewerService> _logger;

        public BrewerService(IHttpClientFactory httpClientFactory, ILogger<BrewerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Beer>> GetBeersAsync()
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);
            var httpRespose = await client.GetAsync("v2/beers");

            httpRespose.EnsureSuccessStatusCode();

            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();

            var beers = list.Select(x => BeerMapper.Map(x));
            return beers;
        }

        public async Task<Beer?> GetBeerAsync(int id)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);
            var httpRespose = await client.GetAsync($"v2/beers/{id}");

            httpRespose.EnsureSuccessStatusCode();

            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is not null && 
                list.Length > 0)
            {
                var beer = BeerMapper.Map(list[0]);
                return beer;
            }
            return null;
        }

        public async Task<Beer?> GetRandomBeerAsync()
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);
            var httpRespose = await client.GetAsync("v2/beers/random");

            httpRespose.EnsureSuccessStatusCode();

            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is not null &&
                list.Length > 0)
            {
                var beer = BeerMapper.Map(list[0]);
                return beer;
            }
            return null;
        }
    }
}
