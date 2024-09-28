using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PDT.CLI;
using PDT.CLI.Configs;
using PDT.CLI.Services;
using PDT.Services;

ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
var builder = Host.CreateApplicationBuilder();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddHttpClient();

builder.Services.AddSingleton<MeiliConfiguration>();
builder.Services.AddSingleton<TikaConfiguration>();

builder.Services.AddScoped<MeilisearchService>();
builder.Services.AddSingleton<RepositoryService>();
builder.Services.AddSingleton<TikaService>();

builder.Services.AddSingleton<test>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddLogging();
var app = builder.Build();

app.Services.GetService<test>();

app.Run();
Console.ReadLine();