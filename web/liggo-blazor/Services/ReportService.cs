using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using liggo_blazor.Models;

namespace liggo_blazor.Services
{
    public class ReportService
    {
        private readonly HttpClient _httpClient;

        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReportStatsDto> GetReportStatsAsync()
        {
            try
            {
                // Get all data needed for calculations
                var attendances = await _httpClient.GetFromJsonAsync<List<AttendanceDto>>("api/Attendances") ?? new List<AttendanceDto>();
                var matches = await _httpClient.GetFromJsonAsync<List<MatchDto>>("api/Matches") ?? new List<MatchDto>();
                var payments = await _httpClient.GetFromJsonAsync<List<PaymentDto>>("api/Payments") ?? new List<PaymentDto>();
                var players = await _httpClient.GetFromJsonAsync<List<PlayerDto>>("api/Players") ?? new List<PlayerDto>();

                // Calculate stats
                var stats = new ReportStatsDto();

                // Attendance average
                if (attendances.Any())
                {
                    var totalAttendance = attendances.Count(a => a.Status == AttendanceStatus.Present);
                    stats.AverageAttendance = (double)totalAttendance / attendances.Count * 100;
                }

                // Goals per match (mock calculation since we don't have goals data yet)
                if (matches.Any())
                {
                    stats.GoalsPerMatch = 3.2; // Placeholder
                }

                // Payments on time
                if (payments.Any())
                {
                    var paidPayments = payments.Count(p => p.Status == "Paid");
                    stats.PaymentsOnTime = (double)paidPayments / payments.Count * 100;
                }

                // Top players (placeholder data)
                stats.TopPlayers = new List<TopPlayerDto>
                {
                    new TopPlayerDto { Name = "Juan Pérez", Goals = 12, Assists = 8, Matches = 15, Rating = 8.5 },
                    new TopPlayerDto { Name = "María García", Goals = 10, Assists = 12, Matches = 14, Rating = 8.7 },
                    new TopPlayerDto { Name = "Carlos López", Goals = 15, Assists = 6, Matches = 16, Rating = 8.2 }
                };

                return stats;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"API request failed: {ex.Message}");
                return new ReportStatsDto { TopPlayers = new List<TopPlayerDto>() };
            }
        }
    }
}