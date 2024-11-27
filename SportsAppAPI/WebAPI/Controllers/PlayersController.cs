using Microsoft.AspNetCore.Mvc;
using SportsAppAPI.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SportsAppAPI.Controllers
{
    [ApiController]
    [Route("/api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IApiSportsClient _apiSportsClient;
        private readonly List<string> _excludedTeamNames;

        public PlayersController(IApiSportsClient apiSportsClient, List<string> excludedTeamNames)
        {
            _apiSportsClient = apiSportsClient;
            _excludedTeamNames = excludedTeamNames;
        }

        // Endpoint to search players by name
        [HttpGet]
        public async Task<IActionResult> SearchPlayers([FromQuery] string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest(new { error = "Search query is required." });
            }

            try
            {
                // Fetch players from the API client
                var players = await _apiSportsClient.SearchPlayersAsync(search);

                // Filter out players with an invalid or "N/A" birth date
                var filteredPlayers = players
                    .Where(player => player.Player.Birth != null && !string.IsNullOrEmpty(player.Player.Birth.Date) && player.Player.Birth.Date != "N/A")
                    .ToList();

                return Ok(filteredPlayers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }



        // Endpoint to get a player by their ID
        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayerProfile([FromRoute] int playerId)
        {
            var playerProfile = await _apiSportsClient.GetPlayerProfileAsync(playerId);
            if (playerProfile == null)
            {
                return NotFound(new { error = "Player not found." });
            }

            return Ok(playerProfile);
        }

        [HttpGet("{playerId}/squads")]
        public async Task<IActionResult> GetPlayerSquads(int playerId)
        {
            var squads = await _apiSportsClient.GetPlayerSquadsAsync(playerId);

            if (squads == null || squads.Count == 0)
            {
                return NotFound($"No squads found for player ID {playerId}.");
            }

            // Filter squads here to exclude those with teams listed in _excludedTeamNames
            var filteredSquads = squads
                .Where(squad => squad.Team != null && !_excludedTeamNames.Contains(squad.Team.Name))
                .ToList();

            // Check if any squads remain after filtering
            if (!filteredSquads.Any())
            {
                return NotFound($"No squads found for player ID {playerId} that are not in the excluded teams.");
            }

            return Ok(filteredSquads);
        }


        [HttpGet("{playerId}/seasons")]
        public async Task<IActionResult> GetPlayerSeasons(int playerId)
        {
            var seasons = await _apiSportsClient.GetPlayerSeasonsAsync(playerId);
            if (seasons == null || !seasons.Any())
            {
                return NotFound($"No seasons found for player ID {playerId}.");
            }

            return Ok(seasons);
        }

        [HttpGet("{playerId}/teams")]
        public async Task<IActionResult> GetFilteredPlayerTeams(int playerId)
        {
            var allTeams = await _apiSportsClient.GetPlayerTeamsAsync(playerId);

            // Filter teams by excluding those with names in the _excludedTeamNames list
            var filteredTeams = allTeams
                .Where(team => !_excludedTeamNames.Contains(team.Team?.Name))
                .ToList();

            return Ok(filteredTeams);
        }

        [HttpGet("{playerId}/statistics")]
        public async Task<IActionResult> GetPlayerStatistics(int playerId)
        {
            try
            {
                // Fetch all seasons for the player
                var seasons = await _apiSportsClient.GetPlayerSeasonsAsync(playerId);

                if (seasons == null || !seasons.Any())
                {
                    return NotFound($"No seasons found for player ID {playerId}.");
                }

                var allStatistics = new List<object>();

                // Fetch statistics for each season
                foreach (var season in seasons)
                {
                    var seasonStatistics = await _apiSportsClient.GetPlayerStatisticsAsync(playerId, season);

                    if (seasonStatistics == null || !seasonStatistics.Any())
                    {
                        continue; // Skip if there are no statistics for the season
                    }

                    // Filter out statistics for excluded teams
                    var filteredStatistics = seasonStatistics
                        .SelectMany(stat => stat.Statistics) // Flatten nested statistics array
                        .Where(innerStat => !_excludedTeamNames.Contains(innerStat.Team?.Name, StringComparer.OrdinalIgnoreCase)) // Filter by team name
                        .ToList();

                    allStatistics.Add(new { Season = season, Statistics = filteredStatistics });
                }

                return Ok(allStatistics); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while fetching player statistics.", details = ex.Message });
            }
        }


        [HttpGet("{playerId}/biggest-season-fixtures")]
        public async Task<IActionResult> GetBiggestSeasonFixtures(int playerId)
        {
            try
            {
                // Fetch the list of seasons
                var seasons = await _apiSportsClient.GetPlayerSeasonsAsync(playerId);
                if (seasons == null || !seasons.Any())
                {
                    return NotFound($"No seasons found for player ID {playerId}.");
                }

                // Get the biggest season year
                var biggestSeason = seasons.Max();
                var currentYear = DateTime.UtcNow.Year;
                if (biggestSeason > currentYear)
                {
                    biggestSeason = currentYear;
                }

                // Fetch the player's squads to find the team ID
                var squads = await _apiSportsClient.GetPlayerSquadsAsync(playerId);

                Console.WriteLine("Squads data for player {0}: {1}", playerId, JsonConvert.SerializeObject(squads));
                if (squads == null || !squads.Any())
                {
                    return NotFound($"No squads found for player ID {playerId}.");
                }

                // Extract the team ID
                var teamId = squads.FirstOrDefault()?.Team?.Id;
                if (teamId == null)
                {
                    return NotFound($"No team ID found for player ID {playerId}.");
                }

                // Fetch fixtures for the biggest season and team ID
                var fixturesResponse = await _apiSportsClient.GetFixturesForSeasonAndTeamAsync(biggestSeason, teamId.Value);

                // Ensure the response contains valid data
                if (fixturesResponse == null || fixturesResponse.Response == null)
                {
                    return NotFound($"No fixtures found for season {biggestSeason} and team ID {teamId}.");
                }

                // Filter fixtures to only include matches before today
                var today = DateTime.UtcNow;
                var pastFixtures = fixturesResponse.Response
                    .Where(f => DateTime.TryParse(f.Fixture?.Date, out var matchDate) && matchDate < today)
                    .ToList();

                // Update the response object to include only filtered matches
                fixturesResponse.Response = pastFixtures;

                return Ok(fixturesResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while fetching data.", details = ex.Message });
            }
        }


        // Endpoint to get top scorers for a specific season and league
        [HttpGet("topscorers")]
        public async Task<IActionResult> GetTopScorers([FromQuery] int season, [FromQuery] int leagueId)
        {
            if (season <= 0 || leagueId <= 0)
            {
                return BadRequest(new { error = "Season and League ID are required and must be valid positive integers." });
            }

            try
            {
                // Fetch top scorers from the API client
                var topScorers = await _apiSportsClient.GetTopScorersForSeasonAndLeagueAsync(season, leagueId);

                if (topScorers == null || !topScorers.Any())
                {
                    return NotFound(new { error = "No top scorers found for the provided season and league." });
                }

                return Ok(topScorers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }



    }
}
