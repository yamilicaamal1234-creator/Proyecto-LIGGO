using System.Net.Http.Json;
using liggo_blazor.Models;

namespace liggo_blazor.Services;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerByIdAsync(int id);
    Task<List<CustomerDto>> GetCustomersByTenantIdAsync(int tenantId);
}

public class CustomerService : ICustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/billing/customers/{id}");
        }
        catch { return null; }
    }

    public async Task<List<CustomerDto>> GetCustomersByTenantIdAsync(int tenantId)
    {
        try
        {
            // Debes tener un endpoint en la API que acepte tenantId
            return await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"api/billing/customers/tenant/{tenantId}") ?? new();
        }
        catch { return new(); }
    }
}
