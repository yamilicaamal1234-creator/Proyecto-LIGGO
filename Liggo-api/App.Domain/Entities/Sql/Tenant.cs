namespace App.Domain.Entities.Sql;

public class Tenant
{
    public int IdTenant { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string WebhookUrl { get; set; } = string.Empty;
    
    public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}