using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace Liggo.Infrastructure.Persistence.Firebase.Models;

[FirestoreData]
public class MemberDocument
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;

    // Puente para realizar las consultas por escuela
    [FirestoreProperty]
    public string SchoolId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Uid { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Role { get; set; } = string.Empty;

    [FirestoreProperty]
    public MemberProfileDocument Profile { get; set; } = new();

    [FirestoreProperty]
    public MemberWalletDocument Wallet { get; set; } = new();

    // Firestore soporta diccionarios nativamente
    [FirestoreProperty]
    public Dictionary<string, DependentSummaryDocument> DependentsSummary { get; set; } = new();
}

[FirestoreData]
public class MemberProfileDocument
{
    [FirestoreProperty] public string Name { get; set; } = string.Empty;
    [FirestoreProperty] public string Phone { get; set; } = string.Empty;
}

[FirestoreData]
public class MemberWalletDocument
{
    [FirestoreProperty] public decimal Balance { get; set; }
    [FirestoreProperty] public string Status { get; set; } = "up_to_date";
    
    // Firestore maneja los DateTime como UTC (Asegúrate de mandarlos como DateTime.UtcNow en Application)
    [FirestoreProperty] public DateTime LastUpdated { get; set; }
}

[FirestoreData]
public class DependentSummaryDocument
{
    [FirestoreProperty] public string Team { get; set; } = string.Empty;
    [FirestoreProperty] public string Photo { get; set; } = string.Empty;
}