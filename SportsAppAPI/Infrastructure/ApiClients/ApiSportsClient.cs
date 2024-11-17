using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsAppAPI.Core.Interfaces;
using SportsAppAPI.Core.Models;

namespace SportsAppAPI.Infrastructure.ApiClients
{
    public class ApiSportsClient : IApiSportsClient
    {
        private readonly HttpClient _httpClient;

        public ApiSportsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

            return apiResponse?.Response ?? new List<FixtureResponse>(); // Return fixtures, or an empty list if none
        }

     
    }
}
