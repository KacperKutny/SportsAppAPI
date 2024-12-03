namespace SportsAppAPI.Core.Models.FixtureEventResponse
{
    public class FixtureEventApiResponse
    {
        public string? Get { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
        public object? Errors { get; set; }
        public int? Results { get; set; }
        public Paging? Paging { get; set; }
        public List<FixtureEventResponse>? Response { get; set; }
    }

    public class Paging
    {
        public int? Current { get; set; }
        public int? Total { get; set; }
    }

    public class FixtureEventResponse
    {
        public TimeResponse? Time { get; set; }
        public TeamResponse? Team { get; set; }
        public PlayerResponse? Player { get; set; }
        public AssistResponse? Assist { get; set; }
        public string? Type { get; set; }
        public string? Detail { get; set; }
        public string? Comments { get; set; }
    }

    public class TimeResponse
    {
        public int? Elapsed { get; set; }
        public int? Extra { get; set; }
    }

    public class TeamResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }

    public class PlayerResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }

    public class AssistResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
