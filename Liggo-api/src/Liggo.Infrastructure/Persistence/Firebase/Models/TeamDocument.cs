using Google.Cloud.Firestore;

namespace Liggo.Infrastructure.Persistence.Firebase.Models;

[FirestoreData]
public class TeamDocument
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;

    // Agregamos SchoolId para poder indexar y buscar los equipos por escuela
    [FirestoreProperty]
    public string SchoolId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Category { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Coach { get; set; } = string.Empty;

    [FirestoreProperty]
    public string LogoUrl { get; set; } = string.Empty;

    [FirestoreProperty]
    public TeamStatsDocument StatsTeam { get; set; } = new();
}

[FirestoreData]
public class TeamStatsDocument
{
    [FirestoreProperty] public int Won { get; set; }
    [FirestoreProperty] public int Lost { get; set; }
    [FirestoreProperty] public int Tied { get; set; }
}