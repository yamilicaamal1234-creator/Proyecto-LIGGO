namespace liggo_blazor.Models;

public class RegisterRequest
{
    public string Id { get; set; } = string.Empty; 
    public AuthDataDto Auth { get; set; } = new();
    public GlobalProfileDto GlobalProfile { get; set; } = new();
    public string ActiveTenantId { get; set; } = string.Empty;
    public List<string> Tenants { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class AuthDataDto
{
    public string Email { get; set; } = string.Empty;
    public string Provider { get; set; } = "email";
    public bool EmailVerified { get; set; } = false;
    public string AccountStatus { get; set; } = "active";
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
}

public class GlobalProfileDto
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
}
