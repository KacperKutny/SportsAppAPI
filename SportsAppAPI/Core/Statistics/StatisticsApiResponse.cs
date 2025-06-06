﻿using System.Collections.Generic;

namespace SportsAppAPI.Core.Models.Statistics
{
    public class StatisticsApiResponse
    {
        public string? Get { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
        public List<object>? Errors { get; set; }
        public int Results { get; set; }
        public Paging? Paging { get; set; }
        public List<PlayerStatisticsResponse>? Response { get; set; }
    }

    public class Paging
    {
        public int Current { get; set; }
        public int Total { get; set; }
    }

    public class PlayerStatisticsResponse
    {
        public Player? Player { get; set; }
        public List<Statistics>? Statistics { get; set; }
    }

    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? Age { get; set; }
        public Birth? Birth { get; set; }
        public string? Nationality { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public bool Injured { get; set; }
        public string? Photo { get; set; }
    }

    public class Birth
    {
        public string? Date { get; set; }
        public string? Place { get; set; }
        public string? Country { get; set; }
    }

    public class Statistics
    {
        public Team? Team { get; set; }
        public League? League { get; set; }
        public Games? Games { get; set; }
        public Substitutes? Substitutes { get; set; }
        public Shots? Shots { get; set; }
        public Goals? Goals { get; set; }
        public Passes? Passes { get; set; }
        public Tackles? Tackles { get; set; }
        public Duels? Duels { get; set; }
        public Dribbles? Dribbles { get; set; }
        public Fouls? Fouls { get; set; }
        public Cards? Cards { get; set; }
        public Penalty? Penalty { get; set; }
    }

    public class Team
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }

    public class League
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Logo { get; set; }
        public string? Flag { get; set; }
        public string? Season { get; set; }
    }

    public class Games
    {
        public int? Appearences { get; set; }
        public int? Lineups { get; set; }
        public int? Minutes { get; set; }
        public int? Number { get; set; }
        public string? Position { get; set; }
        public string? Rating { get; set; }
        public bool Captain { get; set; }
    }

    public class Substitutes
    {
        public int? In { get; set; }
        public int? Out { get; set; }
        public int? Bench { get; set; }
    }

    public class Shots
    {
        public int? Total { get; set; }
        public int? On { get; set; }
    }

    public class Goals
    {
        public int? Total { get; set; }
        public int? Conceded { get; set; }
        public int? Assists { get; set; }
        public int? Saves { get; set; }
    }

    public class Passes
    {
        public int? Total { get; set; }
        public int? Key { get; set; }
        public string? Accuracy { get; set; }
    }

    public class Tackles
    {
        public int? Total { get; set; }
        public int? Blocks { get; set; }
        public int? Interceptions { get; set; }
    }

    public class Duels
    {
        public int? Total { get; set; }
        public int? Won { get; set; }
    }

    public class Dribbles
    {
        public int? Attempts { get; set; }
        public int? Success { get; set; }
        public int? Past { get; set; }
    }

    public class Fouls
    {
        public int? Drawn { get; set; }
        public int? Committed { get; set; }
    }

    public class Cards
    {
        public int? Yellow { get; set; }
        public int? Yellowred { get; set; }
        public int? Red { get; set; }
    }

    public class Penalty
    {
        public int? Won { get; set; }
        public int? Committed { get; set; }
        public int? Scored { get; set; }
        public int? Missed { get; set; }
        public int? Saved { get; set; }
    }
}
