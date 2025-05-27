using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAppAPI.Core.Interfaces;
using SportsAppAPI.Core.Models.Standings;

namespace SportsAppAPI.WebAPI.Controllers
{
    [Route("api/leagues")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly IApiSportsClient _apiSportsClient;


        public LeaguesController(IApiSportsClient apiSportsClient)
        {
            _apiSportsClient = apiSportsClient;
        }

        [HttpGet]
        public async Task<IActionResult> SearchLeagues([FromQuery] string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest(new { error = "Search query is required" });
            }

            try
            {
                var leagues = await _apiSportsClient.SearchLeaguesAsync(search);

                return Ok(leagues);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occured while processing your request", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeagueProfile(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "Invalid league ID" });
            }

            try
            {
                var league = await _apiSportsClient.GetLeagueByIdAsync(id);

                if (league == null)
                {
                    return NotFound(new { error = "League not found" });
                }

                return Ok(league);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request", details = ex.Message });
            }
        }

        [HttpGet("{id}/seasons")]
        public async Task<IActionResult> GetSeasonsForLeague(int id)
        {
            try
            {
                var seasons = await _apiSportsClient.GetSeasonsForLeagueAsync(id);
                return Ok(new { seasons });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}/standings/{season}")]
        public async Task<IActionResult> GetLeagueStandingsForSingleSeason(int id, int season)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "Invalid league ID" });
            }

            if (season <= 0)
            {
                return BadRequest(new { error = "Invalid season year" });
            }

            try
            {
                var standings = await _apiSportsClient.GetStandingsForSingleSeasonAsync(id, season);

                if (standings == null || standings.Count == 0)
                {
                    return NotFound(new { error = $"No standings found for the league in season {season}" });
                }

                return Ok(standings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request", details = ex.Message });
            }
        }



    }
}
