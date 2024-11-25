namespace SportsAppAPI.Core.Models.PlayerTeams
{
    public class PlayerTeamApiResponse
    {
        public string? Get { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
        public List<PlayerTeamResponse>? Response { get; set; }
    }

    public class PlayerTeamResponse
    {
        public Team? Team { get; set; }
        public List<int>? Seasons { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }
}