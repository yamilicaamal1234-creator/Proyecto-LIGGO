using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using liggo_blazor.Models;

namespace liggo_blazor.Services
{
    public class PaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PaymentDto>> GetPaymentsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PaymentDto>>("api/Payments") ?? new();
            }
            catch (HttpRequestException ex)
            {
                System.Console.WriteLine($"API request failed: {ex.Message}");
                return new List<PaymentDto>();
            }
        }

        public async Task CreatePaymentAsync(CreatePaymentRequest paymentRequest)
        {
            var payload = new 
            {
                paymentRequest.PlayerId,
                paymentRequest.Concept,
                paymentRequest.Amount,
                paymentRequest.Date,
                Status = (int)paymentRequest.Status
            };
            var response = await _httpClient.PostAsJsonAsync("api/Payments", payload);
            response.EnsureSuccessStatusCode();
        }
    }
}
