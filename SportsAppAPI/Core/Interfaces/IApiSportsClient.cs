using Microsoft.AspNetCore.Mvc;
using SportsAppAPI.Core.Models.Fixtures;
using SportsAppAPI.Core.Models.PlayerSquad;
using SportsAppAPI.Core.Models.Players;
using SportsAppAPI.Core.Models.PlayerTeams;
using SportsAppAPI.Core.Models.Statistics;
using SportsAppAPI.Core.Models.Transfers;
using SportsAppAPI.Core.Models.Leagues;
using SportsAppAPI.Core.Models.Standings;
using SportsAppAPI.Core.Models.TopScorers;
using SportsAppAPI.Core.Models.FixtureEventResponse;
using SportsAppAPI.Core.Models.FixtureLineups;
using SportsAppAPI.Core.Models.FixtureStatistics;

namespace SportsAppAPI.Core.Interfaces
{
    public interface IApiSportsClient


    {

        Task<List<FixtureResponse>> GetFixturesTodayAsync();

        Task<List<FixtureResponse>> GetFixturesByDateAsync(string date);

        Task<FixtureApiResponse> GetFixturesForMaxSeasonAndLeagueAsync(int leagueId);
       

        Task<List<PlayerProfileResponse>> SearchPlayersAsync(string playerName);
        Task<PlayerProfileResponse> GetPlayerProfileAsync(int playerId);

        Task<List<PlayerSquadResponse>> GetPlayerSquadsAsync(int playerId);

        Task<List<int>> GetPlayerSeasonsAsync(int playerId);

        Task<List<PlayerTeamResponse>> GetPlayerTeamsAsync(int playerId);
        
        Task<List<PlayerStatisticsResponse>> GetPlayerStatisticsAsync(int playerId, int season);

        Task<List<TransfersResponse>> GetPlayerTransfers(int playerId);

        Task<FixtureApiResponse> GetFixturesForSeasonAndTeamAsync(int season,int teamId);

        Task<List<TopScorersResponse>> GetTopScorersForSeasonAndLeagueAsync(int season, int leagueId);

 
        Task<List<LeagueResponse>> SearchLeaguesAsync(string leagueName);

        Task<LeagueResponse> GetLeagueByIdAsync(int leagueId);

        // Standings
        Task<List<StandingResponse>> GetStandingsForSingleSeasonAsync(int leagueId, int season);

        // Seasons
        Task<List<int>> GetSeasonsForLeagueAsync(int leagueId);

        Task<FixtureEventApiResponse> GetFixtureEventsByFixtureIdAsync(int fixtureId);

        Task<FixtureLineupResponse> GetFixtureLineupsByFixtureIdAsync(int fixtureId);

        Task<List<FixtureStatisticsResponse>> GetFixtureStatisticsAsync(int fixtureId);
    }

}