using System.Data.Common;

namespace Liggo.Domain.Entities.Documents
{
    public class Member
    {
        public string Id { get; set; } = string.Empty;
        public string Uid { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public MemberProfile Profile { get; set; } = new();
        public MemberWallet Wallet { get; set; } = new();

        public Dictionary<string, DependentSummary> DependentsSummary { get; set; } = new();
    }

    public class MemberProfile
    {
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }

    public class MemberWallet
    {
        public float Balance { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "up_to_date";
    }
    public class DependentSummary
    {
        public string Name { get; set; } = string.Empty;
        public string Team { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}
