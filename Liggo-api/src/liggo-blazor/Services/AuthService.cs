using System;
using System.Net.Http.Json;
using liggo_blazor.Models;

namespace liggo_blazor.Services;

public class User
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public int TenantId { get; set; } // <--- Aquí guardamos el ID real de MySQL
    public string Avatar { get; set; } = "https://github.com/shadcn.png";
}

public class AuthService
{
    private readonly HttpClient _httpClient;
    public User? CurrentUser { get; private set; } = null;
    public bool IsAuthenticated => CurrentUser != null;

    public event Action? OnChange;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/operations/systemusers", request);
            
            if (response.IsSuccessStatusCode)
            {
                // Al registrar, el backend ya asignó un TenantId
                return await LoginAsync(request.Auth.Email, ""); 
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        try
        {
            // Buscamos el perfil en la API por Email
            var response = await _httpClient.GetAsync($"api/operations/systemusers/email/{email}");
            
            if (response.IsSuccessStatusCode)
            {
                var profile = await response.Content.ReadFromJsonAsync<SystemUserProfileDto>();
                if (profile != null)
                {
                    CurrentUser = new User { 
                        Name = profile.GlobalProfile.FullName,
                        TenantId = int.TryParse(profile.ActiveTenantId, out var tid) ? tid : 0
                    };
                    NotifyStateChanged();
                    return true;
                }
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Login(string name, string role)
    {
        CurrentUser = new User { Name = name, Role = role };
        NotifyStateChanged();
    }

    public void Logout()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    // DTOs internos para mapear la respuesta de la API
    public class SystemUserProfileDto {
        public string ActiveTenantId { get; set; } = "";
        public GlobalProfileDto GlobalProfile { get; set; } = new();
    }
}
