using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using liggo_blazor.Models;

namespace liggo_blazor.Services
{
    public class IncidentService
    {
        private readonly HttpClient _httpClient;

        public IncidentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IncidentDto>> GetIncidentsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<IncidentDto>>("api/Incidents") ?? new();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"API request failed: {ex.Message}");
                return new List<IncidentDto>();
            }
        }

        public async Task CreateIncidentAsync(CreateIncidentRequest incident)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Incidents", incident);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateIncidentAsync(Guid incidentId, UpdateIncidentRequest incident)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Incidents/{incidentId}", incident);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteIncidentAsync(Guid incidentId)
        {
            var response = await _httpClient.DeleteAsync($"api/Incidents/{incidentId}");
            response.EnsureSuccessStatusCode();
        }
    }
}