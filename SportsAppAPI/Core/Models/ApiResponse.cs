namespace SportsAppAPI.Core.Models;
public class ApiResponse
{
    public string Get { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public List<FixtureResponse> Response { get; set; }
}