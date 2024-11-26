namespace SportsAppAPI.Core.Models.Standings
{


    public class StandingApiResponse
    {
        public string? Get { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
        public List<object>? Errors { get; set; }
        public int? Results { get; set; }
        public List<StandingResponse>? Response { get; set; }
    }

    public class StandingResponse
    {
        public League? League { get; set; }
    }

    public class League
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Logo { get; set; }
        public List<List<TeamStanding>>? Standings { get; set; }
    }

    public class TeamStanding
    {
        public int Rank { get; set; }
        public Team? Team { get; set; }
        public int Points { get; set; }
        public int GoalsDiff { get; set; }
        public string? Group { get; set; }
        public string? Form { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public MatchStats? All { get; set; }
        public MatchStats? Home { get; set; }
        public MatchStats? Away { get; set; }
        public string? Update { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }

    public class MatchStats
    {
        public int Played { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public Goals? Goals { get; set; }
    }

    public class Goals
    {
        public int For { get; set; }
        public int Against { get; set; }
    }
}