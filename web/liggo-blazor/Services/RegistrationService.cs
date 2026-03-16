using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using liggo_blazor.Models;

namespace liggo_blazor.Services
{
    public class RegistrationService
    {
        private readonly HttpClient _httpClient;

        public RegistrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RegistrationDto>> GetRegistrationsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RegistrationDto>>("api/Registrations") ?? new();
            }
            catch (HttpRequestException ex)
            {
                System.Console.WriteLine($"API request failed: {ex.Message}");
                return new List<RegistrationDto>();
            }
        }

        public async Task CreateRegistrationAsync(CreateRegistrationRequest registration)
        {
            // The API expects the enum to be sent as a number
            var payload = new 
            {
                registration.PlayerId,
                Plan = (int)registration.Plan,
                registration.StartDate,
                registration.EndDate,
                registration.Amount,
                Status = 1 // Default to Active
            };
            var response = await _httpClient.PostAsJsonAsync("api/Registrations", payload);
            response.EnsureSuccessStatusCode();
        }
    }
}
