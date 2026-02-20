using App.Domain.Enums;

namespace App.Domain.Entities.Sql;

public class Plan
{
    public int IdPlan { get; set; }
    public int IdTenant { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Moneda { get; set; } = "MXN";
    public PaymentFrequency Frecuencia { get; set; }
    public bool Estado { get; set; }
    
    public Tenant? Tenant { get; set; }
}