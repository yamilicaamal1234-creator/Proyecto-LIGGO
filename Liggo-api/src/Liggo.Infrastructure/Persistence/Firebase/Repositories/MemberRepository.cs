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

public class MemberRepository : IMemberRepository
{
    private readonly FirestoreDb _db;
    private const string CollectionName = "members";

    public MemberRepository(FirestoreProvider firestoreProvider)
    {
        _db = firestoreProvider.GetDb();
    }

    public async Task<Member?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var snapshot = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync(cancellationToken);
        if (!snapshot.Exists) return null;

        var doc = snapshot.ConvertTo<MemberDocument>();
        return MapToDomain(doc, snapshot.Id);
    }

    public async Task<IEnumerable<Member>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default)
    {
        // Consulta nativa indexada por SchoolId
        var query = _db.Collection(CollectionName).WhereEqualTo("SchoolId", schoolId);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        return snapshot.Documents
            .Select(d => MapToDomain(d.ConvertTo<MemberDocument>(), d.Id))
            .ToList();
    }

    public async Task AddAsync(Member member, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(member);
        await _db.Collection(CollectionName).Document(member.Id).SetAsync(doc, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Member member, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(member);
        // Usamos MergeAll para no borrar el SchoolId u otros campos que Firebase tenga pero que no bajamos al Dominio
        await _db.Collection(CollectionName).Document(member.Id).SetAsync(doc, SetOptions.MergeAll, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await _db.Collection(CollectionName).Document(id).DeleteAsync(cancellationToken: cancellationToken);
    }

    // ==========================================
    // HELPERS DE MAPEO 
    // ==========================================
    
    private Member MapToDomain(MemberDocument doc, string realId)
    {
        return new Member
        {
            Id = realId,
            Uid = doc.Uid ?? string.Empty,
            Role = doc.Role ?? string.Empty,
            Profile = new MemberProfile
            {
                Name = doc.Profile?.Name ?? string.Empty,
                Phone = doc.Profile?.Phone ?? string.Empty
            },
            Wallet = new MemberWallet
            {
                Balance = doc.Wallet?.Balance ?? 0,
                Status = doc.Wallet?.Status ?? "up_to_date",
                // Si la fecha es por defecto de C# porque Firebase no la regresó, ponemos DateTime.MinValue
                LastUpdated = doc.Wallet?.LastUpdated ?? DateTime.MinValue 
            },
            // Mapeo seguro del Diccionario
            DependentsSummary = doc.DependentsSummary?.ToDictionary(
                k => k.Key,
                v => new DependentSummary
                {
                    Team = v.Value?.Team ?? string.Empty,
                    Photo = v.Value?.Photo ?? string.Empty
                }) ?? new Dictionary<string, DependentSummary>()
        };
    }

    private MemberDocument MapToDocument(Member member)
    {
        return new MemberDocument
        {
            Uid = member.Uid,
            Role = member.Role,
            Profile = new MemberProfileDocument
            {
                Name = member.Profile.Name,
                Phone = member.Profile.Phone
            },
            Wallet = new MemberWalletDocument
            {
                Balance = member.Wallet.Balance,
                Status = member.Wallet.Status,
                // Aseguramos que la fecha vaya en formato UTC, que es el único que acepta Firestore
                LastUpdated = member.Wallet.LastUpdated.Kind == DateTimeKind.Utc 
                              ? member.Wallet.LastUpdated 
                              : member.Wallet.LastUpdated.ToUniversalTime()
            },
            // Mapeo inverso del Diccionario
            DependentsSummary = member.DependentsSummary?.ToDictionary(
                k => k.Key,
                v => new DependentSummaryDocument
                {
                    Team = v.Value.Team,
                    Photo = v.Value.Photo
                }) ?? new Dictionary<string, DependentSummaryDocument>()
        };
    }
}