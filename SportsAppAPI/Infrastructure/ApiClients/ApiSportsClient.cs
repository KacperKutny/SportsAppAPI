using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsAppAPI.Core.Interfaces;

using SportsAppAPI.Core.Models.Fixtures;
using SportsAppAPI.Core.Models.PlayerSquad;
using SportsAppAPI.Core.Models.Players;
using SportsAppAPI.Core.Models.Seasons;
using SportsAppAPI.Core.Models.PlayerTeams;
using SportsAppAPI.Core.Models.Statistics;
using SportsAppAPI.Core.Models.Transfers;

namespace SportsAppAPI.Infrastructure.ApiClients
{
    public class ApiSportsClient : IApiSportsClient
    {
        private readonly HttpClient _httpClient;
        private readonly List<string> _excludedTeamNames;

        public ApiSportsClient(HttpClient httpClient, List<string> excludedTeamNames)
        {
            _httpClient = httpClient;
            _excludedTeamNames = excludedTeamNames;
        }

      

        public async Task<List<FixtureResponse>> GetFixturesTodayAsync()
        {
            var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            return await GetFixturesByDateAsync(today);

        }
        public async Task<List<FixtureResponse>> GetFixturesByDateAsync(string date)
        {
            var endpoint = "/fixtures";

            var requestUri = $"{endpoint}?date={date}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();  // Ensure HTTP success

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<FixtureApiResponse>(jsonResponse);

            return apiResponse?.Response ?? new List<FixtureResponse>(); // Return fixtures, or an empty list if none
        }
       
        
        // Fetch player profiles by name (search functionality)
        public async Task<List<PlayerProfileResponse>> SearchPlayersAsync(string playerName)
        {
            var endpoint = "/players/profiles";
            var requestUri = $"{endpoint}?search={playerName}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode(); // Ensure HTTP success

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<PlayerApiResponse>(jsonResponse);

            return apiResponse?.Response ?? new List<PlayerProfileResponse>(); // Return players or empty list
        }

        // Fetch specific player profile by player ID
        public async Task<PlayerProfileResponse> GetPlayerProfileAsync(int playerId)
        {
            var endpoint = "/players/profiles";
            var requestUri = $"{endpoint}?player={playerId}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode(); // Ensure HTTP success

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<PlayerApiResponse>(jsonResponse);

            return apiResponse?.Response?.FirstOrDefault(); // Return first profile or null
        }


        public async Task<List<PlayerSquadResponse>> GetPlayerSquadsAsync(int playerId)
        {
            var endpoint = "/players/squads";
            var requestUri = $"{endpoint}?player={playerId}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode(); // Ensure HTTP success

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<PlayerSquadApiResponse>(jsonResponse);

            // Apply filtering before returning squads
            var filteredSquads = apiResponse?.Response
                .Where(squad => squad.Team != null && !_excludedTeamNames.Contains(squad.Team.Name))
                .ToList() ?? new List<PlayerSquadResponse>();

            return filteredSquads;
        }


        public async Task<List<int>> GetPlayerSeasonsAsync(int playerId)
        {
            var endpoint = $"/players/seasons?player={playerId}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<SeasonApiResponse>(jsonResponse);

            return apiResponse?.Response ?? new List<int>();
        }
        public async Task<List<PlayerTeamResponse>> GetPlayerTeamsAsync(int playerId)
        {
            var endpoint = $"/players/teams?player={playerId}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<PlayerTeamApiResponse>(jsonResponse);

            return apiResponse?.Response ?? new List<PlayerTeamResponse>();
        }

        public async Task<List<PlayerStatisticsResponse>> GetPlayerStatisticsAsync(int playerId, int season)
        {
            var endpoint = $"/players?id={playerId}&season={season}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<StatisticsApiResponse>(jsonResponse);

            return apiResponse?.Response ?? new List<PlayerStatisticsResponse>();
        }

        public async Task<List<TransfersResponse>> GetPlayerTransfers(int playerId)
        {
            var endpoint = "/transfers";
            var requestUri = $"{endpoint}?player={playerId}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TransfersApiResponse>(jsonResponse);

            return apiResponse?.Response ?? new List<TransfersResponse>();
        }

        public async Task<FixtureApiResponse> GetFixturesForSeasonAndTeamAsync(int season, int teamId)
        {
            var endpoint = $"/fixtures?season={season}&team={teamId}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode(); // Ensure HTTP success

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the entire API response into FixtureApiResponse
            var apiResponse = JsonConvert.DeserializeObject<FixtureApiResponse>(jsonResponse);

            return apiResponse; // Return the full response structure
        }
    }
}
