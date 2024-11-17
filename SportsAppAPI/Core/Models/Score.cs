namespace SportsAppAPI.Core.Models
{
    public class Score
    {
        public Halftime Halftime { get; set; }
        public Fulltime Fulltime { get; set; }
        public Extratime Extratime { get; set; }
        public Penalty Penalty { get; set; }
    }
    public class Halftime
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }

    public class Fulltime
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }

    public class Extratime
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }

    public class Penalty
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }
}
