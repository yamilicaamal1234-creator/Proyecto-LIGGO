using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Liggo.Infrastructure.Persistence.Firebase.Models;

namespace Liggo.Infrastructure.Persistence.Firebase.Repositories;

public class SystemUserRepository : ISystemUserRepository
{
    private readonly FirestoreDb _db;
    private const string CollectionName = "system_users";

    public SystemUserRepository(FirestoreProvider firestoreProvider)
    {
        _db = firestoreProvider.GetDb();
    }

    public async Task<SystemUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var snapshot = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync(cancellationToken);
        if (!snapshot.Exists) return null;

        var doc = snapshot.ConvertTo<SystemUserDocument>();
        return MapToDomain(doc, snapshot.Id);
    }

    public async Task<SystemUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        // Búsqueda en campos anidados de Firestore
        var query = _db.Collection(CollectionName).WhereEqualTo("Auth.Email", email).Limit(1);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        if (snapshot.Documents.Count == 0) return null;

        var document = snapshot.Documents[0];
        return MapToDomain(document.ConvertTo<SystemUserDocument>(), document.Id);
    }

    public async Task<IEnumerable<SystemUser>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default)
    {
        // Búsqueda dentro del arreglo de Tenants
        var query = _db.Collection(CollectionName).WhereArrayContains("Tenants", schoolId);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        return snapshot.Documents
            .Select(d => MapToDomain(d.ConvertTo<SystemUserDocument>(), d.Id))
            .ToList();
    }

    public async Task AddAsync(SystemUser user, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(user);
        await _db.Collection(CollectionName).Document(user.Id).SetAsync(doc, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(SystemUser user, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(user);
        await _db.Collection(CollectionName).Document(user.Id).SetAsync(doc, SetOptions.MergeAll, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await _db.Collection(CollectionName).Document(id).DeleteAsync(cancellationToken: cancellationToken);
    }

    // ==========================================
    // HELPERS DE MAPEO 
    // ==========================================
    
    private SystemUser MapToDomain(SystemUserDocument doc, string realId)
    {
        return new SystemUser
        {
            Id = realId,
            ActiveTenantId = doc.ActiveTenantId ?? string.Empty,
            Tenants = doc.Tenants ?? new List<string>(),
            CreatedAt = doc.CreatedAt == default ? DateTime.MinValue : doc.CreatedAt,
            Auth = new AuthData
            {
                Email = doc.Auth?.Email ?? string.Empty,
                Provider = doc.Auth?.Provider ?? string.Empty,
                EmailVerified = doc.Auth?.EmailVerified ?? false,
                AccountStatus = doc.Auth?.AccountStatus ?? "active",
                LastLogin = doc.Auth != null ? doc.Auth.LastLogin : DateTime.MinValue
            },
            GlobalProfile = new GlobalProfile
            {
                FullName = doc.GlobalProfile?.FullName ?? string.Empty,
                Phone = doc.GlobalProfile?.Phone ?? string.Empty,
                PhotoUrl = doc.GlobalProfile?.PhotoUrl ?? string.Empty
            }
        };
    }

    private SystemUserDocument MapToDocument(SystemUser user)
    {
        return new SystemUserDocument
        {
            ActiveTenantId = user.ActiveTenantId,
            Tenants = user.Tenants,
            CreatedAt = user.CreatedAt.Kind == DateTimeKind.Utc ? user.CreatedAt : user.CreatedAt.ToUniversalTime(),
            Auth = new AuthDataDocument
            {
                Email = user.Auth.Email,
                Provider = user.Auth.Provider,
                EmailVerified = user.Auth.EmailVerified,
                AccountStatus = user.Auth.AccountStatus,
                LastLogin = user.Auth.LastLogin.Kind == DateTimeKind.Utc ? user.Auth.LastLogin : user.Auth.LastLogin.ToUniversalTime()
            },
            GlobalProfile = new GlobalProfileDocument
            {
                FullName = user.GlobalProfile.FullName,
                Phone = user.GlobalProfile.Phone,
                PhotoUrl = user.GlobalProfile.PhotoUrl
            }
        };
    }
}