using Microsoft.AspNetCore.Mvc;
using SportsAppAPI.Core.Models;

namespace SportsAppAPI.Core.Interfaces
{
    public interface IApiSportsClient
    {
        Task<List<FixtureResponse>> GetFixturesTodayAsync();

        Task<List<FixtureResponse>> GetFixturesByDateAsync(string date);

    }
    
}