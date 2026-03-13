namespace Liggo.Domain.Entities.Relational
{
    public class LedgerTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SchoolId { get; set; }
        public Guid MemberId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Concept { get; set; } = string.Empty;
        public Provider Provider { get; set; }
        public string ExternalReference { get; set; } = string.Empty;
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum TransactionType { Charge, Payment }
    public enum Provider { Paypal, OxxoPay, Cash, System}
    public enum Status { Pending, Completed, Failed }
}