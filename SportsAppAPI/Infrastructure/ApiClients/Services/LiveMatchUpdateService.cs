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
                    var liveFixtures = await _apiSportsClient.GetFixturesByDateAsync(DateTime.UtcNow.ToString("yyyy-MM-dd"));
                    var liveGames = liveFixtures
                        .Where(fixture => _leagueIds.Contains(fixture.League.Id) &&
                                          (fixture.Fixture.Status?.Long == "First Half" ||
                                           fixture.Fixture.Status?.Long == "Second Half" ||
                                           fixture.Fixture.Status?.Long == "Extra Time"))
                        .ToList();

                    foreach (var game in liveGames)
                    {
                        var elapsedMinutes = game.Fixture.Status?.Elapsed ?? 0;  
                        var homeGoals = game.Goals?.Home ?? 0;  
                        var awayGoals = game.Goals?.Away ?? 0;  

                        var message = new
                        {
                            fixture = new { id = game.Fixture.Id, status = game.Fixture.Status, date = game.Fixture.Date, elapsedMinutes},
                            league = new { id = game.League.Id, name = game.League.Name, logo = game.League.Logo, flag = game.League.Flag},
                            teams = new { home = new { id = game.Teams.Home.Id, name = game.Teams.Home.Name,logo = game.Teams.Home.Logo, winner = game.Teams.Home.Winner},
                                away = new { id = game.Teams.Away.Id,  name = game.Teams.Away.Name,  logo = game.Teams.Away.Logo,  winner = game.Teams.Away.Winner }
                            },
                            goals = new
                            {
                                home = homeGoals,  
                                away = awayGoals   
                            },
                            score = game.Score
                        };

                        
                        var jsonMessage = JsonSerializer.Serialize(message);

                       
                        await _webSocketHandler.BroadcastMessageAsync(jsonMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in LiveMatchUpdateService: {ex.Message}");
                }

                
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
