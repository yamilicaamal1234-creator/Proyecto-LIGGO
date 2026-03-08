using System;
using Liggo.Domain.Enums;

namespace Liggo.Domain.Entities.Operations
{
    public class Payment : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public string Concept { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentStatus Status { get; set; }

        // Navigation property
        public Player Player { get; set; } = null!;
    }
}
