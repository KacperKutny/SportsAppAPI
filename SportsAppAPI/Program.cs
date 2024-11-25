using SportsAppAPI.Core.Interfaces;
using SportsAppAPI.Infrastructure.ApiClients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using SportsAppAPI.Infrastructure;
using SportsAppAPI.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())  // Make sure the base path is correct
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)  // Read appsettings.json (optional)
    .AddEnvironmentVariables();  // Read environment variables

// Add services to the container.

builder.Services.AddHostedService<LiveMatchUpdateService>();
builder.Services.AddSingleton<WebSocketHandler>();


builder.Services.AddHttpClient<IApiSportsClient, ApiSportsClient>(client =>
{
    var configuration = builder.Configuration;
    var baseUrl = configuration["ApiSports:BaseUrl"];
    var apiKey = configuration["ApiSports:ApiKey"];

    if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(apiKey))
    {
        throw new InvalidOperationException("Base URL or API Key is missing in configuration.");
    }


    client.BaseAddress = new Uri(baseUrl);

    client.DefaultRequestHeaders.Add("x-apisports-key", apiKey);

});

var leagueConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("leagues.json", optional: false, reloadOnChange: true)
    .Build();

var leagueIds = leagueConfig.GetSection("Leagues").Get<List<int>>() ?? new List<int>();

var teamConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("teams.json", optional: false, reloadOnChange: true)
    .Build();

var excludedTeamNames = teamConfig.GetSection("ExcludedTeams").Get<List<string>>() ?? new List<string>();

// Add to the DI container
builder.Services.AddSingleton(excludedTeamNames);

builder.Services.AddSingleton(leagueIds);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React app URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseWebSockets();

app.Map("/ws", (context) =>
{
    var webSocketHandler = context.RequestServices.GetRequiredService<WebSocketHandler>();
    return webSocketHandler.HandleWebSocketAsync(context);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();

app.Run();