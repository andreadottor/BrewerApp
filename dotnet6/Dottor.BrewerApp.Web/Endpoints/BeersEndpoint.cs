namespace Dottor.BrewerApp.Web.Endpoints
{
    using Dottor.BrewerApp.Common.Models;
    using Dottor.BrewerApp.Common.Services;
    using Swashbuckle.AspNetCore.Annotations;

    public static class BeersEndpoint
    {

        public static IEndpointRouteBuilder MapBeersEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/v1/beers", GetBeersAsync)
                        .WithName("Beers")
                        .WithTags("Public")
                        .Produces<IEnumerable<Beer>>(StatusCodes.Status200OK)
                        .WithMetadata(new SwaggerOperationAttribute(
                                summary: "GetBeersAsync",
                                description: "Get beer list"));

            endpoints.MapGet("/api/v1/beers/{id}", GetBeerByIdAsync)
                        .WithName("BeerById")
                        .WithTags("Public")
                        .Produces<Beer>(StatusCodes.Status200OK)
                        .Produces(StatusCodes.Status404NotFound)
                        .WithMetadata(new SwaggerOperationAttribute(
                                summary: "GetBeerByIdAsync",
                                description: "Get beer by id"));

            endpoints.MapGet("/api/v1/beers/random", GetRandomBeerAsync)
                        .WithName("RandomBeer")
                        .WithTags("Public")
                        .Produces<Beer>(StatusCodes.Status200OK)
                        .WithMetadata(new SwaggerOperationAttribute(
                                summary: "GetRandomBeerAsync",
                                description: "Get a random beer"));

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
