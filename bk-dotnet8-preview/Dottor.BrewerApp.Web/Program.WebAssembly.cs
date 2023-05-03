using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using Dottor.BrewerApp.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(BrewerService.BaseUrl) });
builder.Services.AddScoped<IBrewerService, BrewerService>();

await builder.Build().RunAsync();
