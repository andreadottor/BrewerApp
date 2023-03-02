namespace Dottor.BrewerApp.Web.Endpoints
{
    using Dottor.BrewerApp.Common.Models;
    using Dottor.BrewerApp.Common.Services;
    using Dottor.BrewerApp.Web.Extensions;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.Extensions.Logging;

    public static class BeersEndpoint
    {
        public static RouteGroupBuilder MapBeersEndpoint(this IEndpointRouteBuilder endpoints)
        {
            var loggerFactory = endpoints.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("BeersEndpoint");

            var group = endpoints.MapGroup("/api/v1/beers");
            group.WithTags("Public");
            group.WithOpenApi();
            group.RequirePerIPAddressRateLimit();
            group.AddEndpointFilter(async (context, next) =>
            {
                logger.LogInformation("Before first filter");
                var result = await next(context);
                logger.LogInformation("After first filter");
                return result;
            });

            group.MapGet("/", GetBeersAsync)
                        .WithName("Beers") // set the OpenAPI OperationId
                        .WithSummary("GetBeersAsync")
                        .WithDescription("Get beer list");

            group.MapGet("/{id}", GetBeerByIdAsync)
                        .WithOpenApi(generatedOperation =>
                        {
                            generatedOperation.OperationId = "BeerById";
                            generatedOperation.Summary = "GetBeerByIdAsync";
                            generatedOperation.Description = "Get beer by id";

                            var idParameter = generatedOperation.Parameters[0];
                            idParameter.Description = "The ID of the beer.";

                            return generatedOperation;
                        });

            group.MapGet("/random", GetRandomBeerAsync)
                        .WithName("RandomBeer")
                        .WithSummary("GetRandomBeerAsync")
                        .WithDescription("Get a random beer");

            group.MapPost("/", InsertBeersAsync)
                        .WithName("InsertBeer")
                        .WithSummary("InserBeersAsync")
                        .WithDescription("Insert new beer");

            return group;
        }

        public static async Task<Results<Ok<IEnumerable<Beer>>, ProblemHttpResult>> GetBeersAsync(
                                            IBrewerService brewerService,
                                            ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("BeersEndpoint");

            try
            {
                var list = await brewerService.GetBeersAsync();
                return TypedResults.Ok(list);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on retrieve beers list.");
                return TypedResults.Problem(ex.Message, title: "Error on Beers API");
            }
        }

        public static async Task<Results<Ok<Beer>, NotFound, ProblemHttpResult>> GetBeerByIdAsync(
                                            int id,
                                            IBrewerService brewerService,
                                            ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("BeersEndpoint");

            try
            {
                var beer = await brewerService.GetBeerAsync(id);
                if (beer is null)
                    return TypedResults.NotFound();

                return TypedResults.Ok(beer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on retrieve beers list.");
                return TypedResults.Problem(ex.Message, title: "Error on Beers API");
            }
        }

        public static async Task<Results<Ok<Beer>, ProblemHttpResult>> GetRandomBeerAsync(
                                            IBrewerService brewerService,
                                            ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("BeersEndpoint");

            try
            {
                var beer = await brewerService.GetRandomBeerAsync();

                return TypedResults.Ok(beer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on retrieve beers list.");
                return TypedResults.Problem(ex.Message, title: "Error on Beers API");
            }
        }
        public static async Task<Results<Created<Beer>, BadRequest, ProblemHttpResult>> InsertBeersAsync(Beer beer)
        {
            // Fake implementation
            //
            beer.Id = 123;

            return TypedResults.Created($"/api/v1/beers/{beer.Id}", beer);
        }
    }
}
