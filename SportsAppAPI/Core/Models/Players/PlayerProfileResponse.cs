namespace SportsAppAPI.Core.Models.Players
{
    public class PlayerProfileResponse
    {
        public Player? Player { get; set; }
    }

    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? Age { get; set; }
        public BirthInfo? Birth { get; set; }
        public string? Nationality { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Position { get; set; }
        public string? Photo { get; set; }
    }

    public class BirthInfo
    {
        public string? Date { get; set; }
        public string? Place { get; set; }
        public string? Country { get; set; }
    }
}