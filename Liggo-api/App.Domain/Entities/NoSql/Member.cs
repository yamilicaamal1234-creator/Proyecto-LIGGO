namespace App.Domain.Entities.NoSql;

public class Member
{
    public string Id { get; set; } = string.Empty;
    public string Uid { get; set; } = string.Empty; 
    public MemberProfile Profile { get; set; } = new();
    public string Role { get; set; } = "tutor"; 
    public Wallet Wallet { get; set; } = new();
    public Dictionary<string, DependentSummary> DependentsSummary { get; set; } = new();
}

public class MemberProfile
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class Wallet
{
    public decimal Balance { get; set; }
    public string Status { get; set; } = "active"; 
    public DateTime LastUpdated { get; set; }
}

public class DependentSummary
{
    public string Team { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
}