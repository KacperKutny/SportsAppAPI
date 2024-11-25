namespace SportsAppAPI.Core.Models.PlayerSquad
{
    public class PlayerSquadApiResponse
    {
        public string? Get { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
        public List<PlayerSquadResponse>? Response { get; set; }
    }

    public class PlayerSquadResponse
    {
        public Team? Team { get; set; }
        public List<Player>? Players { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }

    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public int? Number { get; set; }
        public string? Position { get; set; }
        public string? Photo { get; set; }
    }
}