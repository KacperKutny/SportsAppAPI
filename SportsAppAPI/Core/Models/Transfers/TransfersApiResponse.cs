namespace SportsAppAPI.Core.Models.Transfers
{
    public class TransfersApiResponse
    {
        public string? Get { get; set; }
        public Parameters? Parameters { get; set; }
        public List<object>? Errors { get; set; }
        public int? Results { get; set; }
        public Paging? Paging { get; set; }
        public List<TransfersResponse>? Response { get; set; }
    }

    public class Parameters
    {
        public string? Player { get; set; }
    }

    public class Paging
    {
        public int? Current { get; set; }
        public int? Total { get; set; }
    }

    public class TransfersResponse
    {
        public Player? Player { get; set; }
        public string? Update { get; set; }
        public List<Transfer>? Transfers { get; set; }
    }

    public class Player
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }

    public class Transfer
    {
        public string? Date { get; set; }
        public string? Type { get; set; }
        public Teams? Teams { get; set; }
    }

    public class Teams
    {
        public Team? In { get; set; }
        public Team? Out { get; set; }
    }

    public class Team
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }
}

