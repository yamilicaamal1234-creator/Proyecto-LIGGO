using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Liggo.Infrastructure.Persistence.Firebase.Models;

namespace Liggo.Infrastructure.Persistence.Firebase.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly FirestoreDb _db;
    private const string CollectionName = "players";

    public PlayerRepository(FirestoreProvider firestoreProvider)
    {
        _db = firestoreProvider.GetDb();
    }

    public async Task<Player?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var snapshot = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync(cancellationToken);
        if (!snapshot.Exists) return null;

        var doc = snapshot.ConvertTo<PlayerDocument>();
        return MapToDomain(doc, snapshot.Id);
    }

    public async Task<IEnumerable<Player>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default)
    {
        // Consulta nativa de Firebase indexada por SchoolId
        var query = _db.Collection(CollectionName).WhereEqualTo("SchoolId", schoolId);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        return snapshot.Documents
            .Select(d => MapToDomain(d.ConvertTo<PlayerDocument>(), d.Id))
            .ToList();
    }

    public async Task<IEnumerable<Player>> GetAllByTeamIdAsync(string teamId, CancellationToken cancellationToken = default)
    {
        // Consulta nativa de Firebase indexada por Team
        var query = _db.Collection(CollectionName).WhereEqualTo("Team", teamId);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        return snapshot.Documents
            .Select(d => MapToDomain(d.ConvertTo<PlayerDocument>(), d.Id))
            .ToList();
    }

    public async Task AddAsync(Player player, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(player);
        await _db.Collection(CollectionName).Document(player.Id).SetAsync(doc, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Player player, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(player);
        await _db.Collection(CollectionName).Document(player.Id).SetAsync(doc, SetOptions.MergeAll, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await _db.Collection(CollectionName).Document(id).DeleteAsync(cancellationToken: cancellationToken);
    }

    // ==========================================
    // HELPERS DE MAPEO (Para mantener todo limpio)
    // ==========================================
    
    private Player MapToDomain(PlayerDocument doc, string realId)
    {
        return new Player
        {
            Id = realId,
            Status = doc.Status ?? "active",
            Team = doc.Team ?? string.Empty,
            Parents = doc.Parents ?? new List<string>(),
            Info = new PlayerInfo
            {
                Name = doc.Info?.Name ?? string.Empty,
                Dob = doc.Info?.Dob ?? string.Empty,
                Gender = doc.Info?.Gender ?? string.Empty,
                PhotoUrl = doc.Info?.PhotoUrl ?? string.Empty
            },
            Stats = new PlayerStats
            {
                Matches = doc.Stats?.Matches ?? 0,
                Goals = doc.Stats?.Goals ?? 0,
                Minutes = doc.Stats?.Minutes ?? 0,
                YellowCards = doc.Stats?.YellowCards ?? 0,
                RedCards = doc.Stats?.RedCards ?? 0
            }
        };
    }

    private PlayerDocument MapToDocument(Player player)
    {
        return new PlayerDocument
        {
            Info = new PlayerInfoDocument
            {
                Name = player.Info.Name,
                Dob = player.Info.Dob,
                Gender = player.Info.Gender,
                PhotoUrl = player.Info.PhotoUrl
            },
            Status = player.Status,
            Team = player.Team,
            Parents = player.Parents,
            Stats = new PlayerStatsDocument
            {
                Matches = player.Stats.Matches,
                Goals = player.Stats.Goals,
                Minutes = player.Stats.Minutes,
                YellowCards = player.Stats.YellowCards,
                RedCards = player.Stats.RedCards
            }
        };
    }
}