using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using liggo_blazor.Models;

namespace liggo_blazor.Services
{
    public class PlayerService
    {
        private readonly HttpClient _httpClient;

        public PlayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlayerDto>> GetPlayersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PlayerDto>>("api/Players") ?? new();
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions (e.g., log them, return empty list)
                System.Console.WriteLine($"API request failed: {ex.Message}");
                return new List<PlayerDto>();
            }
        }

        public async Task CreatePlayerAsync(CreatePlayerRequest player)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Players", player);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePlayerAsync(System.Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Players/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
