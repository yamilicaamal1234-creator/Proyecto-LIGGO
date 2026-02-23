using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace Liggo.Infrastructure.Persistence.Firebase.Models;

[FirestoreData]
public class SystemUserDocument
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty]
    public AuthDataDocument Auth { get; set; } = new();

    [FirestoreProperty]
    public GlobalProfileDocument GlobalProfile { get; set; } = new();

    [FirestoreProperty]
    public string ActiveTenantId { get; set; } = string.Empty;

    [FirestoreProperty]
    public List<string> Tenants { get; set; } = new();

    [FirestoreProperty]
    public DateTime CreatedAt { get; set; }
}

[FirestoreData]
public class AuthDataDocument
{
    [FirestoreProperty] public string Email { get; set; } = string.Empty;
    [FirestoreProperty] public string Provider { get; set; } = string.Empty;
    [FirestoreProperty] public bool EmailVerified { get; set; }
    [FirestoreProperty] public string AccountStatus { get; set; } = "active";
    [FirestoreProperty] public DateTime LastLogin { get; set; }
}

[FirestoreData]
public class GlobalProfileDocument
{
    [FirestoreProperty] public string FullName { get; set; } = string.Empty;
    [FirestoreProperty] public string Phone { get; set; } = string.Empty;
    [FirestoreProperty] public string PhotoUrl { get; set; } = string.Empty;
}