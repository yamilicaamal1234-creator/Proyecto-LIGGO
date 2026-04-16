using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using liggo_blazor.Models;
using static liggo_blazor.Services.AuthService;

namespace liggo_blazor.Services
{
    public class ApiSimulatorHandler : DelegatingHandler
    {
        private static readonly List<PlayerDto> _players = new()
        {
            new PlayerDto { Id = Guid.NewGuid(), FullName = "Juan Pérez", DateOfBirth = new DateTime(2005, 5, 20), AssignedTeam = "Los Tigres", GuardianName = "Carlos Pérez", GuardianPhone = "555-0101", Relationship = "Padre", CreatedAt = DateTime.Now },
            new PlayerDto { Id = Guid.NewGuid(), FullName = "Miguel Gómez", DateOfBirth = new DateTime(2006, 8, 15), AssignedTeam = "Los Leones", GuardianName = "Ana Gómez", GuardianPhone = "555-0202", Relationship = "Madre", CreatedAt = DateTime.Now }
        };

        private static readonly List<MatchDto> _matches = new()
        {
            new MatchDto { Id = Guid.NewGuid(), LocalTeam = "Los Tigres", VisitingTeam = "Los Leones", DateTime = DateTime.Now.AddDays(2), Location = "Cancha 1", Category = MatchCategory.Sub15.ToString() }
        };

        private static readonly List<CustomerDto> _customers = new()
        {
            new CustomerDto { Id = 1, Name = "Academia Central", ContactEmail = "contacto@academia.com", Status = "Active" }
        };

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var path = request.RequestUri?.AbsolutePath.ToLower() ?? string.Empty;

            try
            {
                // Auth: Login
                if (path.Contains("api/operations/systemusers/email/"))
                {
                    // Anonymous type that matches the expected JSON structure
                    var profile = new 
                    {
                        ActiveTenantId = "1",
                        GlobalProfile = new { FullName = "Admin Simulator" }
                    };
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(profile) };
                }
                
                // Auth: Register
                if (path.Contains("api/operations/systemusers") && request.Method == HttpMethod.Post)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                // Players GET
                if (path.Contains("api/players") && request.Method == HttpMethod.Get)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(_players) };
                }
                
                // Players POST
                if (path.Contains("api/players") && request.Method == HttpMethod.Post)
                {
                    if (request.Content != null)
                    {
                        var newPlayer = await request.Content.ReadFromJsonAsync<CreatePlayerRequest>(cancellationToken: cancellationToken);
                        if (newPlayer != null)
                        {
                            _players.Add(new PlayerDto 
                            { 
                                Id = Guid.NewGuid(), 
                                FullName = newPlayer.FullName, 
                                DateOfBirth = newPlayer.DateOfBirth ?? DateTime.Today,
                                AssignedTeam = newPlayer.AssignedTeam,
                                GuardianName = newPlayer.GuardianName,
                                GuardianPhone = newPlayer.GuardianPhone,
                                Relationship = newPlayer.Relationship,
                                CreatedAt = DateTime.Now
                            });
                        }
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                
                // Players DELETE
                if (path.Contains("api/players") && request.Method == HttpMethod.Delete)
                {
                    var idString = path.Split('/').LastOrDefault();
                    if (Guid.TryParse(idString, out var id))
                    {
                        _players.RemoveAll(p => p.Id == id);
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                // Matches GET
                if (path.Contains("api/matches") && request.Method == HttpMethod.Get)
                {
                    if (path.Split('/').Length > 3) // GET /id
                    {
                        var idStr = path.Split('/').LastOrDefault();
                        if (Guid.TryParse(idStr, out var id))
                        {
                            var match = _matches.FirstOrDefault(m => m.Id == id);
                            return new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(match) };
                        }
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(_matches) };
                }
                
                // Matches POST
                if (path.Contains("api/matches") && request.Method == HttpMethod.Post)
                {
                    if (request.Content != null)
                    {
                        var newMatch = await request.Content.ReadFromJsonAsync<CreateMatchRequest>(cancellationToken: cancellationToken);
                        if (newMatch != null)
                        {
                            _matches.Add(new MatchDto 
                            { 
                                Id = Guid.NewGuid(), 
                                LocalTeam = newMatch.LocalTeam, 
                                VisitingTeam = newMatch.VisitingTeam,
                                DateTime = newMatch.DateTime ?? DateTime.Now,
                                Location = newMatch.Location,
                                Category = newMatch.Category.ToString()
                            });
                        }
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                // Matches DELETE
                if (path.Contains("api/matches") && request.Method == HttpMethod.Delete)
                {
                    var idString = path.Split('/').LastOrDefault();
                    if (Guid.TryParse(idString, out var id))
                    {
                        _matches.RemoveAll(m => m.Id == id);
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                // Customers GET
                if (path.Contains("api/billing/customers/tenant/"))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(_customers) };
                }
                if (path.Contains("api/billing/customers/"))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(_customers.FirstOrDefault()) };
                }

                // Fallback for everything else to avoid API errors during development
                if (request.Method == HttpMethod.Get)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(new List<object>()) };
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Simple error handling for simulator
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                return errorResponse;
            }
        }
    }
}
