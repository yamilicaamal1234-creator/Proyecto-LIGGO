using System;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace Liggo.Infrastructure.Persistence.Firebase;

public class FirestoreProvider
{
    private readonly FirestoreDb _firestoreDb;

    public FirestoreProvider(IConfiguration configuration)
    {
        // Lee el ProjectId de tu appsettings.json
        var projectId = configuration["Firebase:ProjectId"];
        
        if (string.IsNullOrWhiteSpace(projectId))
            throw new ArgumentNullException("Firebase:ProjectId", "El Project ID de Firebase no está configurado en el appsettings.json.");

        // IMPORTANTE: En producción, Google busca automáticamente la variable de entorno 
        // GOOGLE_APPLICATION_CREDENTIALS con la ruta al archivo .json de tus claves.
        _firestoreDb = FirestoreDb.Create(projectId);
    }

    public FirestoreDb GetDb() => _firestoreDb;
}