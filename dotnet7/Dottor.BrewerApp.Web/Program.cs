using Dottor.BrewerApp.Common;
using Dottor.BrewerApp.Web.Components;
using Dottor.BrewerApp.Web.Endpoints;
using Dottor.BrewerApp.Web.Extensions;
using Microsoft.AspNetCore.Components.Web;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.RootComponents.RegisterCustomElement<BeerList>("beer-list");
});

builder.Services.AddBrewerServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "BrewerApp API (.NET 7)",
        Version = "v1"
    });
});

builder.Services.AddRateLimiting();
builder.Services.AddOutputCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseOutputCache();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRateLimiter();

app.MapBeersEndpoint();
app.MapRazorPages();
app.MapBlazorHub();

app.Run();
