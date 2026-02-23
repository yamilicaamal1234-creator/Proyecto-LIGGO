using Liggo.Domain.Enums;

namespace Liggo.Domain.Entities.Billing;

public class Plan
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "MXN";
    public PlanFrequency Frequency { get; set; }
    public bool IsActive { get; set; } = true;

    // Propiedades de navegación
    public Tenant Tenant { get; set; } = null!;
}