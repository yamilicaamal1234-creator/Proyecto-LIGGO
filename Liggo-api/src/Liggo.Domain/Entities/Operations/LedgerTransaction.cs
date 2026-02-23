namespace Liggo.Domain.Entities.Operations;

public class LedgerTransaction
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // "charge" o "payment"
    public decimal Amount { get; set; }
    
    // Campos condicionales dependiendo del Type
    public string Concept { get; set; } = string.Empty; // Para "charge"
    public string Method { get; set; } = string.Empty;  // Para "payment"
    public string TransactionRef { get; set; } = string.Empty; // Para "payment"

    public TransactionRelatedUsers RelatedUsers { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class TransactionRelatedUsers
{
    public string PayerName { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
}