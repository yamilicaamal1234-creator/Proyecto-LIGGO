namespace Liggo.Domain.Entities.Operations;

public class SystemUser
{
    public string Id { get; set; } = string.Empty;
    public AuthData Auth { get; set; } = new();
    public GlobalProfile GlobalProfile { get; set; } = new();
    public string ActiveTenantId { get; set; } = string.Empty;
    public List<string> Tenants { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class AuthData
{
    public string Email { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty; // "email", "google"
    public bool EmailVerified { get; set; }
    public string AccountStatus { get; set; } = "active";
    public DateTime LastLogin { get; set; }
}

public class GlobalProfile
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
}