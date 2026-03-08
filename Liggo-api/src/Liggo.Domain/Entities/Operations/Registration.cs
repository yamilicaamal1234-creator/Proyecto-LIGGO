using System;
using Liggo.Domain.Enums;

namespace Liggo.Domain.Entities.Operations
{
    public class Registration : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public PaymentPlan Plan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public RegistrationStatus Status { get; set; }

        // Navigation property
        public Player Player { get; set; } = null!;
    }
}
