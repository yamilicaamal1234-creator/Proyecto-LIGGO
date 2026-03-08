using System;

namespace liggo_blazor.Models
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public string Concept { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
