namespace Liggo.Domain.Entities.Operations;

// Estos records solucionan los errores de tipos no encontrados (CS0246)

public record TransactionRelatedUsers
{
    public string StudentName { get; init; } = string.Empty;
    public string PayerName { get; init; } = string.Empty;
}

public record MemberProfile
{
    public string Name { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public record MemberWallet
{
    public decimal Balance { get; set; }
    public string Currency { get; set; } = "USD";
    public string Status { get; set; } = "up_to_date";
    public DateTime LastUpdated { get; set; }
}

public record DependentSummary
{
    public string FullName { get; init; } = string.Empty;
    public string Relationship { get; init; } = string.Empty;
    public string Team { get; init; } = string.Empty;
    public string Photo { get; init; } = string.Empty;
}

public record SchoolInfo
{
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Plan { get; init; } = string.Empty;
    public string LogoUrl { get; init; } = string.Empty;
}

public record SchoolSettings
{
    public string Currency { get; set; } = "MXN";
    public string Timezone { get; set; } = "America/Mexico_City";
    public List<CategoryInfo> Categories { get; set; } = new();
}

public record CategoryInfo
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<int> Years { get; init; } = new();
}