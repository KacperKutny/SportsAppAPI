using Microsoft.AspNetCore.Mvc;
using SportsAppAPI.Core.Interfaces;

namespace SportsAppAPI.WebAPI.Controllers
{

    [ApiController]
    [Route("/api/transfers")]
    public class TransfersController : ControllerBase
    {
        private readonly IApiSportsClient _apiSportsClient;

  
        public TransfersController(IApiSportsClient apiSportsClient)
        {
            _apiSportsClient = apiSportsClient;

        }
        

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayerTransfers([FromRoute] int playerId)
        {
            var playerTransfers = await _apiSportsClient.GetPlayerTransfers(playerId);
            if (playerTransfers == null || !playerTransfers.Any())
            {
                return NotFound(new { error = "Transfers not found." });
            }
            return Ok(playerTransfers);
        }
    }
}
