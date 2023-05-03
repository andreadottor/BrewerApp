namespace Dottor.BrewerApp.Web.Services;

using Dottor.BrewerApp.Dtos;
using Dottor.BrewerApp.Web.Models;
using Dottor.BrewerApp.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

    private readonly HttpClient _httpClient;
    private readonly ILogger<BrewerService> _logger;

    public BrewerService(HttpClient httpClient, ILogger<BrewerService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<Beer>> GetBeersAsync()
    {
        var httpRespose = await _httpClient.GetAsync("v2/beers");

        if (httpRespose.IsSuccessStatusCode)
        {
            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();
            if(list is null)
                return Enumerable.Empty<Beer>();

            var beers = list.Select(x => BeerMapper.Map(x));
            return beers;
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/'. Response status code: {StatusCode} {ReasonPhrase}", (int)httpRespose.StatusCode, httpRespose.ReasonPhrase);
        }

        return Enumerable.Empty<Beer>();
    }

    public async Task<Beer?> GetBeerAsync(int id)
    {
        var httpRespose = await _httpClient.GetAsync($"v2/beers/{id}");

        if (httpRespose.IsSuccessStatusCode)
        {
            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is not null && list.Length > 0)
            {
                var beer = BeerMapper.Map(list[0]);
                return beer;
            }
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/{id}'. Response status code: {StatusCode} {ReasonPhrase}", id, (int)httpRespose.StatusCode, httpRespose.ReasonPhrase);
        }

        return null;
    }

    public async Task<Beer> GetRandomBeerAsync()
    {
        var httpRespose = await _httpClient.GetAsync("v2/beers/random");
        
        if (httpRespose.IsSuccessStatusCode)
        {
            var list = await httpRespose.Content.ReadFromJsonAsync<BeerDto[]>();
            if (list is not null &&
                list.Length > 0)
            {
                var beer = BeerMapper.Map(list[0]);
                return beer;
            }
        }
        else
        {
            _logger.LogError("Error on call 'v2/beers/random'. Response status code: {StatusCode} {ReasonPhrase}", (int)httpRespose.StatusCode, httpRespose.ReasonPhrase);
        }

        throw new Exception("Random beer API return no beer.");
    }
}
