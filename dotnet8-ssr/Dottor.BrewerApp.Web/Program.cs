using Dottor.BrewerApp.Web.Shared;
using Dottor.BrewerApp.Common;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
builder.Services.AddBrewerServices();
builder.Services.AddRazorComponents();


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorComponents<MainLayout>();

app.Run();
