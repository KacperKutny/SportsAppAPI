using Microsoft.AspNetCore.Mvc;
using SportsAppAPI.Core.Interfaces;
using System.Threading.Tasks;

namespace WebAPI.SportsAppAPI.Controllers
{
    [ApiController]
    [Route("/api/fixtures")]
    public class FixturesController : ControllerBase
    {
        private readonly IApiSportsClient _apiSportsClient;
        private readonly List<int> _leagueIds;

        public FixturesController(IApiSportsClient apiSportsClient, List<int> leagueIds)
        {
            _apiSportsClient = apiSportsClient;
            _leagueIds = leagueIds;
        }

        [HttpGet]
        public async Task<IActionResult> GetFixturesByDate([FromQuery] string date)
        {
            var allFixtures = await _apiSportsClient.GetFixturesByDateAsync(date); // Fetch fixtures for the given date

            // Filter fixtures based on the league IDs and status (finished or not started)
            var filteredFixtures = allFixtures
                .Where(fixtureResponse =>
                    _leagueIds.Contains(fixtureResponse.League.Id))
                .ToList();

            return Ok(filteredFixtures);
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetTodayFixtures()
        {
            var allFixtures = await _apiSportsClient.GetFixturesTodayAsync();

            var filteredFixtures = allFixtures
               .Where(fixture => _leagueIds.Contains(fixture.League.Id))
               .ToList();

            return Ok(filteredFixtures);
        }

        [HttpGet("max-season/{leagueId}")]
        public async Task<IActionResult> GetFixturesForMaxSeason(int leagueId)
        {
            var fixtureApiResponse = await _apiSportsClient.GetFixturesForMaxSeasonAndLeagueAsync(leagueId);

            if (fixtureApiResponse?.Response == null || !fixtureApiResponse.Response.Any())
            {
                return NotFound("No fixtures found for the latest season.");
            }

            return Ok(fixtureApiResponse.Response);
        }

        [HttpGet("{fixtureId}/events")]
        public async Task<IActionResult> GetFixtureEvents(int fixtureId)
        {
            var fixtureEventApiResponse = await _apiSportsClient.GetFixtureEventsByFixtureIdAsync(fixtureId);

            if (fixtureEventApiResponse?.Response == null || !fixtureEventApiResponse.Response.Any())
            {
                return NotFound("No events found for this fixture.");
            }

            return Ok(fixtureEventApiResponse.Response);
        }

        [HttpGet("{fixtureId}/lineups")]
        public async Task<IActionResult> GetFixtureLineups(int fixtureId)
        {
            var fixtureLineupApiResponse = await _apiSportsClient.GetFixtureLineupsByFixtureIdAsync(fixtureId);

            if (fixtureLineupApiResponse?.Response == null || !fixtureLineupApiResponse.Response.Any())
            {
                return NotFound("No lineups found for this fixture.");
            }

            return Ok(fixtureLineupApiResponse.Response);
        }

        [HttpGet("{fixtureId}/statistics")]
        public async Task<IActionResult> GetFixtureStatistics(int fixtureId)
        {
            var statistics = await _apiSportsClient.GetFixtureStatisticsAsync(fixtureId);

            if (statistics == null || !statistics.Any())
            {
                return NotFound($"No statistics found for fixture with ID: {fixtureId}");
            }

            return Ok(statistics);
        }

    }
}