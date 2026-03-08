using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Liggo.Domain.Entities.Operations;

public class LedgerTransaction : BaseEntity
{
    public Guid SchoolId { get; set; } // Foreign key to School
    public Guid MemberId { get; set; } // Foreign key to Member (Payer)
    public string Type { get; set; } = string.Empty; // "charge" o "payment"
    public decimal Amount { get; set; }
    
    // Campos condicionales dependiendo del Type
    public string Concept { get; set; } = string.Empty; // Para "charge"
    public string Method { get; set; } = string.Empty;  // Para "payment"
    public string TransactionRef { get; set; } = string.Empty; // Para "payment"

    public string PayerName { get; set; } = string.Empty; // Flattened from TransactionRelatedUsers
    public string StudentName { get; set; } = string.Empty; // Flattened from TransactionRelatedUsers

    [NotMapped]
    public TransactionRelatedUsers RelatedUsers { get; set; } = new();

    // Navigation properties
    public School School { get; set; } = null!;
    public Member Member { get; set; } = null!;
}