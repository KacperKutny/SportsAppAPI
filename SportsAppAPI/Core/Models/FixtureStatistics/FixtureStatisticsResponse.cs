namespace SportsAppAPI.Core.Models.FixtureStatistics
{
    public class FixtureStatisticsResponse
    {
        public TeamStatistics? Team { get; set; }
        public List<StatisticDetail>? Statistics { get; set; }
    }

    public class TeamStatistics
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }

    public class StatisticDetail
    {
        public string? Type { get; set; }
        public object? Value { get; set; }
    }

    public class FixtureStatisticsApiResponse
    {
        public string? Get { get; set; }
        public object? Parameters { get; set; }
        public List<object>? Errors { get; set; }
        public int Results { get; set; }
        public object? Paging { get; set; }
        public List<FixtureStatisticsResponse>? Response { get; set; }
    }
}

