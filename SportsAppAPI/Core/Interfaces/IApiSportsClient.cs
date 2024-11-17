using SportsAppAPI.Core.Models;

namespace SportsAppAPI.Core.Interfaces
{
    public interface IApiSportsClient
    {
        Task<List<FixtureResponse>> GetFixturesTodayAsync();

    }
    
}