using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace Liggo.Infrastructure.Persistence.Firebase.Models;

[FirestoreData]
public class PlayerDocument
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;

    // Nota: Aunque en tu clase de dominio no estaba explícito el SchoolId, 
    // lo agregamos al documento de Firebase porque tus consultas (Queries) lo necesitan para filtrar.
    [FirestoreProperty]
    public string SchoolId { get; set; } = string.Empty;

    [FirestoreProperty]
    public PlayerInfoDocument Info { get; set; } = new();

    [FirestoreProperty]
    public string Status { get; set; } = "active";

    [FirestoreProperty]
    public string Team { get; set; } = string.Empty;

    [FirestoreProperty]
    public List<string> Parents { get; set; } = new();

    [FirestoreProperty]
    public PlayerStatsDocument Stats { get; set; } = new();
}

[FirestoreData]
public class PlayerInfoDocument
{
    [FirestoreProperty] public string Name { get; set; } = string.Empty;
    [FirestoreProperty] public string Dob { get; set; } = string.Empty;
    [FirestoreProperty] public string Gender { get; set; } = string.Empty;
    [FirestoreProperty] public string PhotoUrl { get; set; } = string.Empty;
}

[FirestoreData]
public class PlayerStatsDocument
{
    [FirestoreProperty] public int Matches { get; set; }
    [FirestoreProperty] public int Goals { get; set; }
    [FirestoreProperty] public int Minutes { get; set; }
    [FirestoreProperty] public int YellowCards { get; set; }
    [FirestoreProperty] public int RedCards { get; set; }
}   