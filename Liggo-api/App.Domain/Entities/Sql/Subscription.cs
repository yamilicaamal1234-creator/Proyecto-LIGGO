using App.Domain.Enums;

namespace App.Domain.Entities.Sql;

public class Subscription
{
    public int IdSub { get; set; }
    public int IdCustomer { get; set; }
    public int IdPlan { get; set; }
    
    public SubscriptionStatus Status { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public bool RenovacionAuto { get; set; }
    
    public Customer? Customer { get; set; }
    public Plan? Plan { get; set; }
}