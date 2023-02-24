namespace Dottor.BrewerApp.Web.Extensions;

using System.Net;
using System.Threading.RateLimiting;

public static class RateLimitExtensions
{
    private static readonly string Policy = "PerIPAddressRatelimit";

    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        return services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy(Policy, context =>
            {
                IPAddress? remoteIpAddress = context.Connection.RemoteIpAddress;

                //if (IPAddress.IsLoopback(remoteIpAddress!))
                //{
                return RateLimitPartition.GetTokenBucketLimiter(remoteIpAddress!, key =>
                {
                    return new()
                    {
                        ReplenishmentPeriod = TimeSpan.FromSeconds(5),
                        AutoReplenishment = true,
                        TokenLimit = 5,
                        TokensPerPeriod = 2,
                        QueueLimit = 2,
                    };
                });
                //}
                //return RateLimitPartition.GetNoLimiter(IPAddress.Loopback);
            });
        });
    }

    public static IEndpointConventionBuilder RequirePerIPAddressRateLimit(this IEndpointConventionBuilder builder)
    {
        return builder.RequireRateLimiting(Policy);
    }
}