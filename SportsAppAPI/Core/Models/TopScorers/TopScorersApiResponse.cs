using System;
using System.Collections.Generic;
namespace SportsAppAPI.Core.Models.TopScorers
{
    public class TopScorersApiResponse
    {
        public string? Get { get; set; } // API endpoint
        public Parameters? Parameters { get; set; } // Parameters object
        public List<object>? Errors { get; set; } // List of errors, if any
        public int? Results { get; set; } // Number of results
        public Paging? Paging { get; set; } // Paging information
        public List<TopScorersResponse>? Response { get; set; } // List of top scorers
    }

    public class Parameters
    {
        public string? League { get; set; } // League ID
        public string? Season { get; set; } // Season year
    }

    public class Paging
    {
        public int? Current { get; set; } // Current page
        public int? Total { get; set; } // Total pages
    }

    public class TopScorersResponse
    {
        public Player? Player { get; set; } // Player information
        public List<Statistic>? Statistics { get; set; } // List of statistics
    }

    public class Player
    {
        public int? Id { get; set; } // Player ID
        public string? Name { get; set; } // Full name
        public string? Firstname { get; set; } // First name
        public string? Lastname { get; set; } // Last name
        public int? Age { get; set; } // Player age
        public Birth? Birth { get; set; } // Birth information
        public string? Nationality { get; set; } // Nationality
        public string? Height { get; set; } // Height (e.g., "194 cm")
        public string? Weight { get; set; } // Weight (e.g., "88 kg")
        public bool? Injured { get; set; } // Injury status
        public string? Photo { get; set; } // URL to player photo
    }

    public class Birth
    {
        public string? Date { get; set; } // Date of birth
        public string? Place { get; set; } // Birthplace
        public string? Country { get; set; } // Country of birth
    }

    public class Statistic
    {
        public Team? Team { get; set; } // Team information
        public League? League { get; set; } // League information
        public Games? Games { get; set; } // Game stats
        public Substitutes? Substitutes { get; set; } // Substitute stats
        public Shots? Shots { get; set; } // Shot stats
        public Goals? Goals { get; set; } // Goal stats
        public Passes? Passes { get; set; } // Passing stats
        public Tackles? Tackles { get; set; } // Tackling stats
        public Duels? Duels { get; set; } // Duel stats
        public Dribbles? Dribbles { get; set; } // Dribble stats
        public Fouls? Fouls { get; set; } // Foul stats
        public Cards? Cards { get; set; } // Card stats
        public Penalty? Penalty { get; set; } // Penalty stats
    }

    public class Team
    {
        public int? Id { get; set; } // Team ID
        public string? Name { get; set; } // Team name
        public string? Logo { get; set; } // URL to team logo
    }

    public class League
    {
        public int? Id { get; set; } // League ID
        public string? Name { get; set; } // League name
        public string? Country { get; set; } // Country of the league
        public string? Logo { get; set; } // URL to league logo
        public string? Flag { get; set; } // URL to league's flag
        public int? Season { get; set; } // Season year
    }

    public class Games
    {
        public int? Appearences { get; set; } // Number of games played
        public int? Lineups { get; set; } // Number of times in starting lineup
        public int? Minutes { get; set; } // Total minutes played
        public object? Number { get; set; } // Jersey number
        public string? Position { get; set; } // Playing position
        public string? Rating { get; set; } // Player rating
        public bool? Captain { get; set; } // Captaincy status
    }

    public class Substitutes
    {
        public int? In { get; set; } // Times substituted in
        public int? Out { get; set; } // Times substituted out
        public int? Bench { get; set; } // Times on the bench
    }

    public class Shots
    {
        public int? Total { get; set; } // Total shots
        public int? On { get; set; } // Shots on target
    }

    public class Goals
    {
        public int? Total { get; set; } // Total goals
        public int? Conceded { get; set; } // Goals conceded
        public object? Assists { get; set; } // Assists
        public object? Saves { get; set; } // Saves
    }

    public class Passes
    {
        public int? Total { get; set; } // Total passes
        public int? Key { get; set; } // Key passes
        public object? Accuracy { get; set; } // Pass accuracy
    }

    public class Tackles
    {
        public int? Total { get; set; } // Total tackles
        public object? Blocks { get; set; } // Blocks
        public object? Interceptions { get; set; } // Interceptions
    }

    public class Duels
    {
        public int? Total { get; set; } // Total duels
        public int? Won { get; set; } // Duels won
    }

    public class Dribbles
    {
        public int? Attempts { get; set; } // Dribble attempts
        public int? Success { get; set; } // Successful dribbles
        public object? Past { get; set; } // Times dribbled past
    }

    public class Fouls
    {
        public int? Drawn { get; set; } // Fouls drawn
        public int? Committed { get; set; } // Fouls committed
    }

    public class Cards
    {
        public int? Yellow { get; set; } // Yellow cards
        public int? Yellowred { get; set; } // Yellow-red cards
        public int? Red { get; set; } // Red cards
    }

    public class Penalty
    {
        public object? Won { get; set; } // Penalties won
        public object? Commited { get; set; } // Penalties committed
        public int? Scored { get; set; } // Penalties scored
        public int? Missed { get; set; } // Penalties missed
        public object? Saved { get; set; } // Penalties saved
    }

}
