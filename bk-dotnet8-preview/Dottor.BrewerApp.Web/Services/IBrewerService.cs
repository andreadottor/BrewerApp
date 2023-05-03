namespace Dottor.BrewerApp.Web.Services;

using Dottor.BrewerApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBrewerService
{
    Task<IEnumerable<Beer>> GetBeersAsync();

    Task<Beer?> GetBeerAsync(int id);

    Task<Beer> GetRandomBeerAsync();
}
