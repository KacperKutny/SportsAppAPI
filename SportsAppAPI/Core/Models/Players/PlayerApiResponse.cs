using System.Collections.Generic;

namespace SportsAppAPI.Core.Models.Players
{
    public class PlayerApiResponse
    {
        public string Get { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public List<PlayerProfileResponse> Response { get; set; }
    }
}
