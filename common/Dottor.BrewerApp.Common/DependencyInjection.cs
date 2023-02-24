namespace Dottor.BrewerApp.Common;

using Dottor.BrewerApp.Common.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddBrewerServices(this IServiceCollection services)
    {
        services.AddHttpClient(BrewerService.HttpClientName, client =>
        {
            client.BaseAddress = new Uri(BrewerService.BaseUrl);
        });

        services.AddSingleton<IBrewerService, BrewerService>();

    }
}