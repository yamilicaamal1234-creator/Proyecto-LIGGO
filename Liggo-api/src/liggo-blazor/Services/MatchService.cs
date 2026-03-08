using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using liggo_blazor.Models;

namespace liggo_blazor.Services
{
    public class MatchService
    {
        private readonly HttpClient _httpClient;

        public MatchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MatchDto>> GetMatchesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MatchDto>>("api/Matches") ?? new();
            }
            catch (HttpRequestException ex)
            {
                System.Console.WriteLine($"API request failed: {ex.Message}");
                return new List<MatchDto>();
            }
        }

        public async Task<MatchDto?> GetMatchByIdAsync(System.Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MatchDto>($"api/Matches/{id}");
            }
            catch (HttpRequestException ex)
            {
                System.Console.WriteLine($"API request failed: {ex.Message}");
                return null;
            }
        }

        public async Task CreateMatchAsync(CreateMatchRequest match)
        {
            var payload = new 
            {
                match.LocalTeam,
                match.VisitingTeam,
                match.DateTime,
                match.Location,
                Category = (int)match.Category
            };
            var response = await _httpClient.PostAsJsonAsync("api/Matches", payload);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMatchAsync(System.Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Matches/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
