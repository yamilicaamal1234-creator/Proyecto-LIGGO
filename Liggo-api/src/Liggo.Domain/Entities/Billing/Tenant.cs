namespace Liggo.Domain.Entities.Billing;

public class Tenant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string WebhookUrl { get; set; } = string.Empty;

    // Propiedades de navegación (Relaciones de EF Core)
    public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}