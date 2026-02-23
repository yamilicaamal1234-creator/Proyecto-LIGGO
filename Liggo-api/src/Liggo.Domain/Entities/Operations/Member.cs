namespace Liggo.Domain.Entities.Operations;

public class Member
{
    public string Id { get; set; } = string.Empty; // ID del documento
    public string Uid { get; set; } = string.Empty; // Referencia a SystemUser
    public MemberProfile Profile { get; set; } = new();
    public string Role { get; set; } = string.Empty; // "tutor", "coach", "admin"
    public MemberWallet Wallet { get; set; } = new();
    
    // Diccionario para el patrón Dependents Summary (Clave: Nombre/ID del hijo)
    public Dictionary<string, DependentSummary> DependentsSummary { get; set; } = new();
}

public class MemberProfile
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class MemberWallet
{
    public decimal Balance { get; set; }
    public string Status { get; set; } = "up_to_date"; // "up_to_date", "overdue"
    public DateTime LastUpdated { get; set; }
}

public class DependentSummary
{
    public string Team { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
}