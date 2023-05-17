namespace Dottor.BrewerApp.Common.Services;

using Dottor.BrewerApp.Common.Dtos;
using Dottor.BrewerApp.Common.Models;
using Dottor.BrewerApp.Common.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
        var httpResponse = await client.GetAsync("v2/beers");

        if (httpResponse.IsSuccessStatusCode)
        {
            var list = await httpResponse.Content.ReadFromJsonAsync<BeerDto[]>();
            if(list is null)
                return Enumerable.Empty<Beer>();

            var beers = list.Select(x => BeerMapper.Map(x));
            return beers;
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/'. Response status code: {StatusCode} {ReasonPhrase}", (int)httpResponse.StatusCode, httpResponse.ReasonPhrase);
        }

        return Enumerable.Empty<Beer>();
    }

    public async Task<Beer?> GetBeerAsync(int id)
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var httpResponse = await client.GetAsync($"v2/beers/{id}");

        if (httpResponse.IsSuccessStatusCode)
        {
            var list = await httpResponse.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is not null && list.Length > 0)
            {
                var beer = BeerMapper.Map(list[0]);
                return beer;
            }
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/{id}'. Response status code: {StatusCode} {ReasonPhrase}", id, (int)httpResponse.StatusCode, httpResponse.ReasonPhrase);
        }

        return null;
    }

    public async Task<Beer> GetRandomBeerAsync()
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var httpResponse = await client.GetAsync("v2/beers/random");
        
        if (httpResponse.IsSuccessStatusCode)
        {
            var list = await httpResponse.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is not null &&
                list.Length > 0)
            {
                var beer = BeerMapper.Map(list[0]);
                return beer;
            }
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/random'. Response status code: {StatusCode} {ReasonPhrase}", (int)httpResponse.StatusCode, httpResponse.ReasonPhrase);
        }

        throw new Exception("Random beer API return no beer.");
    }
}
