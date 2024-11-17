using System.Net.Http;
using System.Threading.Tasks;
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
            var endpoint = "/fixtures";

            var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var requestUri = $"{endpoint}?date={today}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

            return apiResponse?.Response ?? new List<FixtureResponse>();

        }
      
    }
}
