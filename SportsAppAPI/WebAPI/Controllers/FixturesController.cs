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

    }
}