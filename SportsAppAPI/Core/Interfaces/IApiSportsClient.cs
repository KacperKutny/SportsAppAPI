using Microsoft.AspNetCore.Mvc;
using SportsAppAPI.Core.Models.Fixtures;
using SportsAppAPI.Core.Models.PlayerSquad;
using SportsAppAPI.Core.Models.Players;
using SportsAppAPI.Core.Models.PlayerTeams;
using SportsAppAPI.Core.Models.Statistics;
using SportsAppAPI.Core.Models.Transfers;

namespace SportsAppAPI.Core.Interfaces
{
    public interface IApiSportsClient
    {
        Task<List<FixtureResponse>> GetFixturesTodayAsync();

        Task<List<FixtureResponse>> GetFixturesByDateAsync(string date);

        Task<List<PlayerProfileResponse>> SearchPlayersAsync(string playerName);
        Task<PlayerProfileResponse> GetPlayerProfileAsync(int playerId);

        Task<List<PlayerSquadResponse>> GetPlayerSquadsAsync(int playerId);

        Task<List<int>> GetPlayerSeasonsAsync(int playerId);

        Task<List<PlayerTeamResponse>> GetPlayerTeamsAsync(int playerId);

        Task<List<PlayerStatisticsResponse>> GetPlayerStatisticsAsync(int playerId, int season);

        Task<List<TransfersResponse>> GetPlayerTransfers(int playerId);

        Task<FixtureApiResponse> GetFixturesForSeasonAndTeamAsync(int season,int teamId);


    }

}