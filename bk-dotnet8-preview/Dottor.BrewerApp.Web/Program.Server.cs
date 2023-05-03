using Dottor.BrewerApp.Web.Shared;
using Dottor.BrewerApp.Web.Services;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(BrewerService.BaseUrl) });
builder.Services.AddScoped<IBrewerService, BrewerService>();

builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents();

var app = builder.Build();

app.UseBlazorFrameworkFiles(); // Enable WebAssembly
app.UseStaticFiles();
app.UseRouting();

app.MapRazorComponents();
app.MapBlazorHub();

app.Run();
