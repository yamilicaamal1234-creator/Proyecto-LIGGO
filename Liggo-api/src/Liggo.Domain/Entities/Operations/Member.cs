using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Liggo.Domain.Entities.Operations;

public class Member : BaseEntity
{
    public Guid SchoolId { get; set; } // Foreign key to School
    public string Uid { get; set; } = string.Empty; // Referencia a SystemUser

    // Flattened MemberProfile properties
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    // Flattened MemberWallet properties
    public decimal Balance { get; set; }
    public string WalletStatus { get; set; } = "up_to_date"; // "up_to_date", "overdue"
    public DateTime LastWalletUpdate { get; set; }
    
    // Complex properties for Handlers
    [NotMapped]
    public MemberProfile Profile { get; set; } = new();
    public string Role { get; set; } = string.Empty;
    [NotMapped]
    public MemberWallet Wallet { get; set; } = new();
    [NotMapped]
    public Dictionary<string, DependentSummary> DependentsSummary { get; set; } = new();

    // Navigation properties
    public School School { get; set; } = null!;
    public ICollection<LedgerTransaction> LedgerTransactions { get; set; } = new List<LedgerTransaction>();
}