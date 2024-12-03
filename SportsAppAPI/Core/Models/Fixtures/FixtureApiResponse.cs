﻿namespace SportsAppAPI.Core.Models.Fixtures
{
    public class FixtureApiResponse
    {
        public string? Get { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
        public object? Errors { get; set; }
        public int? Results { get; set; }
        public Paging? Paging { get; set; }
        public List<FixtureResponse>? Response { get; set; }
    }

    public class Paging
    {
        public int? Current { get; set; }
        public int? Total { get; set; }
    }

    public class FixtureResponse
    {
        public Fixture? Fixture { get; set; }
        public League? League { get; set; }
        public Teams? Teams { get; set; }
        public Goals? Goals { get; set; }
        public Score? Score { get; set; }
    }

    public class Fixture
    {
        public int? Id { get; set; }
        public string? Referee { get; set; }
        public string? Timezone { get; set; }
        public string? Date { get; set; }
        public long? Timestamp { get; set; }
        public Periods? Periods { get; set; }
        public Venue? Venue { get; set; }
        public Status? Status { get; set; }
    }

    public class Periods
    {
        public long? First { get; set; }
        public long? Second { get; set; }
    }

    public class Venue
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
    }

    public class Status
    {
        public string? Long { get; set; }
        public string? Short { get; set; }
        public int? Elapsed { get; set; }
    }

    public class League
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Logo { get; set; }
        public string? Flag { get; set; }
        public int? Season { get; set; }
        public string? Round { get; set; }
    }

    public class Teams
    {
        public TeamDetail? Home { get; set; }
        public TeamDetail? Away { get; set; }
    }

    public class TeamDetail
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public bool? Winner { get; set; }
    }

    public class Goals
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }

    public class Score
    {
        public HalfTime? Halftime { get; set; }
        public HalfTime? Fulltime { get; set; }
        public HalfTime? Extratime { get; set; }
        public HalfTime? Penalty { get; set; }
    }

    public class HalfTime
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }
}