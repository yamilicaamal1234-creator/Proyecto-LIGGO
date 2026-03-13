namespace Liggo.Domain.Entities.Relational
{
    public class School
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public int  PlanId { get; set; }

        public SubscriptionStatus SubscriptionStatus { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string TimeZone { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum SubscriptionStatus { Active, PastDue, Canceled }
}