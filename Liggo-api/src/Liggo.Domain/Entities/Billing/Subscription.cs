using System;
using Liggo.Domain.Enums;

namespace Liggo.Domain.Entities.Billing;

public class Subscription
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int PlanId { get; set; }
    
    public SubscriptionStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool AutoRenew { get; set; }

    // Propiedades de navegación
    public Customer Customer { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
}