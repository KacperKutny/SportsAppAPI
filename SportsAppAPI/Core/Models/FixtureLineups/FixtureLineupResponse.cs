namespace SportsAppAPI.Core.Models.FixtureLineups
{
    public class FixtureLineupResponse
    {
        // Nullable list of teams' lineup data
        public List<TeamLineup>? Response { get; set; }

        // Pagination info
        public PagingInfo? Paging { get; set; }

        // TeamLineup model matches the structure for each team in the response
        public class TeamLineup
        {
            public Team? Team { get; set; } // The team details, can be null
            public Coach? Coach { get; set; } // Coach details, can be null
            public string? Formation { get; set; } // Formation, can be null
            public List<PlayerLineup>? StartXI { get; set; } // List of starting XI, can be null
            public List<PlayerLineup>? Substitutes { get; set; } // List of substitutes, can be null
        }

        // Team class contains the team's details such as id, name, and colors
        public class Team
        {
            public int? Id { get; set; } // Team ID, nullable
            public string? Name { get; set; } // Team name, nullable
            public string? Logo { get; set; } // Team logo URL, nullable
            public TeamColors? Colors { get; set; } // Colors, can be null
        }

        // TeamColors model for storing player and goalkeeper colors
        public class TeamColors
        {
            public ColorDetails? Player { get; set; } // Player colors, can be null
            public ColorDetails? Goalkeeper { get; set; } // Goalkeeper colors, can be null
        }

        // ColorDetails for player and goalkeeper colors (primary, number, border)
        public class ColorDetails
        {
            public string? Primary { get; set; } // Primary color, nullable
            public string? Number { get; set; } // Number color, nullable
            public string? Border { get; set; } // Border color, nullable
        }

        // Coach class to store coach details (id, name, photo)
        public class Coach
        {
            public int? Id { get; set; } // Coach ID, nullable
            public string? Name { get; set; } // Coach name, nullable
            public string? Photo { get; set; } // Coach photo URL, nullable
        }

        // PlayerLineup class for each player in the lineup
        public class PlayerLineup
        {
            public PlayerDetails? Player { get; set; } // Player details, can be null
        }

        // PlayerDetails for storing player's id, name, number, position, and grid
        public class PlayerDetails
        {
            public int? Id { get; set; } // Player ID, nullable
            public string? Name { get; set; } // Player name, nullable
            public int? Number { get; set; } // Player number, nullable
            public string? Position { get; set; } // Player position (e.g., G, D, M, F), nullable
            public string? Grid { get; set; } // Player grid position, nullable (can be null as per the API response)
        }

        // PagingInfo class to store pagination details
        public class PagingInfo
        {
            public int? Current { get; set; } // Current page, nullable
            public int? Total { get; set; } // Total number of pages, nullable
        }
    }
}
