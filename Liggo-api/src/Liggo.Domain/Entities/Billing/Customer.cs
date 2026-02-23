namespace Liggo.Domain.Entities.Billing;

public class Customer
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    
    // CRÍTICO: Aquí guardamos el orgId de Firebase (ej. "school_rayados")
    public string ExternalId { get; set; } = string.Empty; 
    
    public string BusinessName { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty; // RFC
    public string AdminEmail { get; set; } = string.Empty;

    // Propiedades de navegación
    public Tenant Tenant { get; set; } = null!;
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}