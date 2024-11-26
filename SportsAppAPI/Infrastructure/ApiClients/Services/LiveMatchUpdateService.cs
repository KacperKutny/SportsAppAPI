using SportsAppAPI.Core.Interfaces;
using SportsAppAPI.Core.Models;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportsAppAPI.Infrastructure.Services
{
    public class LiveMatchUpdateService : BackgroundService
    {
        private readonly IApiSportsClient _apiSportsClient;
        private readonly WebSocketHandler _webSocketHandler;
        private readonly List<int> _leagueIds;

        public LiveMatchUpdateService(IApiSportsClient apiSportsClient, WebSocketHandler webSocketHandler, List<int> leagueIds)
        {
            _apiSportsClient = apiSportsClient;
            _webSocketHandler = webSocketHandler;
            _leagueIds = leagueIds;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Fetch live fixtures
                    var liveFixtures = await _apiSportsClient.GetFixturesByDateAsync(DateTime.UtcNow.ToString("yyyy-MM-dd"));

                    // Filter live matches by league and status
                    var liveGames = liveFixtures
                        .Where(fixture => _leagueIds.Contains(fixture.League.Id) &&
                                          (fixture.Fixture.Status?.Long == "First Half" ||
                                           fixture.Fixture.Status?.Long == "Second Half" ||
                                           fixture.Fixture.Status?.Long == "Extra Time"))
                        .ToList();

                    foreach (var game in liveGames)
                    {
                        // Prepare full match data for WebSocket broadcast
                        var message = new
                        {
                            fixture = new
                            {
                                id = game.Fixture.Id,
                                status = game.Fixture.Status,
                                date = game.Fixture.Date
                            },
                            league = new
                            {
                                id = game.League.Id,
                                name = game.League.Name,
                                logo = game.League.Logo,
                                flag = game.League.Flag
                            },
                            teams = new
                            {
                                home = new
                                {
                                    id = game.Teams.Home.Id,
                                    name = game.Teams.Home.Name,
                                    logo = game.Teams.Home.Logo,
                                    winner = game.Teams.Home.Winner
                                },
                                away = new
                                {
                                    id = game.Teams.Away.Id,
                                    name = game.Teams.Away.Name,
                                    logo = game.Teams.Away.Logo,
                                    winner = game.Teams.Away.Winner
                                }
                            },
                            goals = game.Goals,
                            score = game.Score
                        };

                        // Serialize to JSON
                        var jsonMessage = JsonSerializer.Serialize(message);

                        // Send the message to all connected clients
                        await _webSocketHandler.BroadcastMessageAsync(jsonMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in LiveMatchUpdateService: {ex.Message}");
                }

                // Wait for 30 seconds before fetching updates again
                await Task.Delay(60100, stoppingToken);
            }
        }
    }
}
