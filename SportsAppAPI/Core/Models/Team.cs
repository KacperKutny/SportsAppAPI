namespace SportsAppAPI.Core.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public bool? Winner { get; set; }
    }
}