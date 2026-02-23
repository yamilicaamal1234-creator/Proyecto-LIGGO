using System;
using Google.Cloud.Firestore;

namespace Liggo.Infrastructure.Persistence.Firebase.Models;

[FirestoreData]
public class LedgerTransactionDocument
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;

    // Campos índice para las consultas de la interfaz
    [FirestoreProperty]
    public string SchoolId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string MemberId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Type { get; set; } = string.Empty; // "charge" o "payment"

    // IMPORTANTE: Firestore usa double para números con decimales, no soporta 'decimal' directo
    [FirestoreProperty]
    public double Amount { get; set; }

    [FirestoreProperty]
    public string Concept { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Method { get; set; } = string.Empty;

    [FirestoreProperty]
    public string TransactionRef { get; set; } = string.Empty;

    [FirestoreProperty]
    public TransactionRelatedUsersDocument RelatedUsers { get; set; } = new();

    // Firestore requiere que las fechas sean UTC
    [FirestoreProperty]
    public DateTime CreatedAt { get; set; }
}

[FirestoreData]
public class TransactionRelatedUsersDocument
{
    [FirestoreProperty] public string PayerName { get; set; } = string.Empty;
    [FirestoreProperty] public string StudentName { get; set; } = string.Empty;
}