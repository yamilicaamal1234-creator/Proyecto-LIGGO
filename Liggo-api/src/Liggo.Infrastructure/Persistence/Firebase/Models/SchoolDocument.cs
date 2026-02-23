using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace Liggo.Infrastructure.Persistence.Firebase.Models;

[FirestoreData]
public class SchoolDocument
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty]
    public SchoolInfoDocument Info { get; set; } = new();

    [FirestoreProperty]
    public SchoolSettingsDocument Settings { get; set; } = new();
}

[FirestoreData]
public class SchoolInfoDocument
{
    [FirestoreProperty] public string Name { get; set; } = string.Empty;
    [FirestoreProperty] public string Plan { get; set; } = string.Empty;
    [FirestoreProperty] public string LogoUrl { get; set; } = string.Empty;
}

[FirestoreData]
public class SchoolSettingsDocument
{
    [FirestoreProperty] public string Currency { get; set; } = "MXN";
    [FirestoreProperty] public string Timezone { get; set; } = "America/Mexico_City";
    [FirestoreProperty] public List<CategoryInfoDocument> Categories { get; set; } = new();
}

[FirestoreData]
public class CategoryInfoDocument
{
    [FirestoreProperty] public string Name { get; set; } = string.Empty;
    [FirestoreProperty] public List<int> Years { get; set; } = new();
}