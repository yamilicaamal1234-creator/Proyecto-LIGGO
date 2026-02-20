namespace App.Domain.Entities.NoSql;

public class SystemUser
{
    public string Uid { get; set; } = string.Empty;
    public AuthInfo Auth { get; set; } = new();
    public GlobalProfile GlobalProfile { get; set; } = new();
    public string ActiveTenantId { get; set; } = string.Empty;
    public List<string> Tenants { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class AuthInfo
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; 
    public string Provider { get; set; } = "email";
    public bool EmailVerified { get; set; }
    public string AccountStatus { get; set; } = "active";
    public int FailedAttempts { get; set; }
    public DateTime? LastLogin { get; set; }
}

public class GlobalProfile
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
}