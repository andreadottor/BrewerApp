namespace Dottor.BrewerApp.Common.Services;

using Dottor.BrewerApp.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBrewerService
{
    Task<IEnumerable<Beer>> GetBeersAsync();

    Task<Beer?> GetBeerAsync(int id);

    Task<Beer> GetRandomBeerAsync();
}
