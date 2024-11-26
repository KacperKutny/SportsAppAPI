using System;
using System.Collections.Generic;

namespace SportsAppAPI.Core.Models.Leagues;

public class LeagueApiResponse
{
    public string? Get { get; set; }
    public Dictionary<string, string>? Parameters { get; set; }
    public List<object>? Errors { get; set; }
    public int? Results { get; set; }
    public Paging? Paging { get; set; }
    public List<LeagueResponse>? Response { get; set; }
}


public class Paging
{
    public int? Current { get; set; }
    public int? Total { get; set; }
}

public class LeagueResponse
{
    public League? League { get; set; }
    public Country? Country { get; set; }
    public List<Season>? Seasons { get; set; }
}

public class League
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Logo { get; set; }
}

public class Country
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Flag { get; set; }
}

public class Season
{
    public int Year { get; set; }
    public string Start { get; set; }
    public string? End { get; set; }
    public bool Current { get; set; }
    public Coverage Coverage { get; set; }
}

public class Coverage
{
    public Fixtures Fixtures { get; set; }
    public bool Standings { get; set; }
    public bool Players { get; set; }
    public bool TopScorers { get; set; }
    public bool TopAssists { get; set; }
    public bool TopCards { get; set; }
    public bool Injuries { get; set; }
    public bool Predictions { get; set; }
    public bool Odds { get; set; }
}

public class Fixtures
{
    public bool Events { get; set; }
    public bool Lineups { get; set; }
    public bool Statistics_Fixtures { get; set; }
    public bool Statistics_Players { get; set; }
}
