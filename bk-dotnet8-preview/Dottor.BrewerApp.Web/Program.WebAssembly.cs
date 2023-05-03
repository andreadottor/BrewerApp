// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using Dottor.BrewerApp.Common.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(BrewerService.BaseUrl) });
builder.Services.AddScoped<IBrewerService, BrewerService>();

await builder.Build().RunAsync();
