using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using liggo_blazor.Models;

namespace liggo_blazor.Services
{
    public class AttendanceService
    {
        private readonly HttpClient _httpClient;

        public AttendanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AttendanceDto>> GetAttendancesAsync(Guid matchId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<AttendanceDto>>($"api/Attendances/{matchId}") ?? new();
            }
            catch (HttpRequestException ex)
            {
                System.Console.WriteLine($"API request failed: {ex.Message}");
                return new List<AttendanceDto>();
            }
        }

        public async Task SaveAttendanceAsync(SaveAttendanceRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Attendances", request);
            response.EnsureSuccessStatusCode();
        }
    }
}
