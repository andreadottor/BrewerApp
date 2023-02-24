namespace Dottor.BrewerApp.Web.Endpoints
{
    using Dottor.BrewerApp.Common.Models;
    using Dottor.BrewerApp.Common.Services;
    using Dottor.BrewerApp.Web.Extensions;

    public static class BeersEndpoint
    {

        public static IEndpointRouteBuilder AddBeersEndpoint(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/api/v1/beers");
            group.WithTags("Public");
            // Rate limit all of the APIs
            group.RequirePerIPAddressRateLimit();

            group.MapGet("", GetBeersAsync)
                        .WithName("Beers")
                        .Produces<IEnumerable<Beer>>(StatusCodes.Status200OK)
                        .WithSummary("GetBeersAsync")
                        .WithDescription("Get beer list")
                        .WithOpenApi();

            group.MapGet("{id}", GetBeerByIdAsync)
                        .WithName("BeerById")
                        .Produces<Beer>(StatusCodes.Status200OK)
                        .Produces(StatusCodes.Status404NotFound)
                        .WithSummary("GetBeerByIdAsync")
                        .WithDescription("Get beer by id")
                        .WithOpenApi(generatedOperation =>
                        {
                            var parameter = generatedOperation.Parameters[0];
                            parameter.Description = "The ID of the beer.";
                            return generatedOperation;
                        }); ;

            group.MapGet("random", GetRandomBeerAsync)
                        .WithName("RandomBeer")
                        .Produces<Beer>(StatusCodes.Status200OK)
                        .WithSummary("GetRandomBeerAsync")
                        .WithDescription("Get a random beer")
                        .WithOpenApi();

            return endpoints;
        }

        public static async Task<IResult> GetBeersAsync(
                                        IBrewerService brewerService,
                                        ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("BeersEndpoint");

            try
            {
                var list = await brewerService.GetBeersAsync();
                return Results.Ok(list);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on retrieve beers list.");
                return Results.Problem(ex.Message, title: "Error on Beers API");
            }
        }

        public static async Task<IResult> GetBeerByIdAsync(
                                        int id,
                                        IBrewerService brewerService,
                                        ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("BeersEndpoint");

            try
            {
                var beer = await brewerService.GetBeerAsync(id);
                if (beer is null)
                    return Results.NotFound();

                return Results.Ok(beer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on retrieve beers list.");
                return Results.Problem(ex.Message, title: "Error on Beers API");
            }
        }

        public static async Task<IResult> GetRandomBeerAsync(
                                IBrewerService brewerService,
                                ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("BeersEndpoint");

            try
            {
                var beer = await brewerService.GetRandomBeerAsync();

                return Results.Ok(beer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on retrieve beers list.");
                return Results.Problem(ex.Message, title: "Error on Beers API");
            }
        }
    }
}
