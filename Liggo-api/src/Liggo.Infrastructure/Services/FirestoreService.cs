using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Liggo.Application.Interfaces;

namespace Liggo.Infrastructure.Services;

public class FirestoreService : IFirestoreTenantRepository
{
    
    private readonly FirestoreDb _db;

    public FirestoreService(IConfiguration configuration)
    {
        var projectId = configuration["Firebase:ProjectId"];
        _db = FirestoreDb.Create(projectId);
    }

    public async Task CreateTenantStructureAsync(string tenantId, string name)
    {
        var tenantRef = _db.Collection("tenants").Document(tenantId);

        await tenantRef.SetAsync(new
        {
            info = new
            {
                name = name,
                createdAt = Timestamp.GetCurrentTimestamp()
            }
        });
    }
}