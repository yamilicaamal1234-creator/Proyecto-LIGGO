namespace App.Domain.Entities.Sql;

public class Customer
{
    public int IdCustomer { get; set; }
    public int IdTenant { get; set; }
    public string ExternalId { get; set; } = string.Empty; 
    public string RazonSocial { get; set; } = string.Empty;
    public string RfcTaxId { get; set; } = string.Empty;
    public string EmailAdmin { get; set; } = string.Empty;
    
    public Tenant? Tenant { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}