using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Liggo.Infrastructure.Persistence.Firebase.Models;

namespace Liggo.Infrastructure.Persistence.Firebase.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly FirestoreDb _db;
    private const string CollectionName = "teams";

    public TeamRepository(FirestoreProvider firestoreProvider)
    {
        _db = firestoreProvider.GetDb();
    }

    public async Task<Team?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var snapshot = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync(cancellationToken);
        if (!snapshot.Exists) return null;

        var doc = snapshot.ConvertTo<TeamDocument>();
        return MapToDomain(doc, snapshot.Id);
    }

    public async Task<IEnumerable<Team>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default)
    {
        // Consulta nativa de Firebase indexada por SchoolId
        var query = _db.Collection(CollectionName).WhereEqualTo("SchoolId", schoolId);
        var snapshot = await query.GetSnapshotAsync(cancellationToken);

        return snapshot.Documents
            .Select(d => MapToDomain(d.ConvertTo<TeamDocument>(), d.Id))
            .ToList();
    }

    public async Task AddAsync(Team team, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(team);
        await _db.Collection(CollectionName).Document(team.Id).SetAsync(doc, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Team team, CancellationToken cancellationToken = default)
    {
        var doc = MapToDocument(team);
        // Usamos MergeAll para no sobreescribir ni borrar el SchoolId si se actualiza el equipo
        await _db.Collection(CollectionName).Document(team.Id).SetAsync(doc, SetOptions.MergeAll, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await _db.Collection(CollectionName).Document(id).DeleteAsync(cancellationToken: cancellationToken);
    }

    // ==========================================
    // HELPERS DE MAPEO 
    // ==========================================
    
    private Team MapToDomain(TeamDocument doc, string realId)
    {
        return new Team
        {
            Id = realId,
            Name = doc.Name ?? string.Empty,
            Category = doc.Category ?? string.Empty,
            Coach = doc.Coach ?? string.Empty,
            LogoUrl = doc.LogoUrl ?? string.Empty,
            StatsTeam = new TeamStats
            {
                Won = doc.StatsTeam?.Won ?? 0,
                Lost = doc.StatsTeam?.Lost ?? 0,
                Tied = doc.StatsTeam?.Tied ?? 0
            }
        };
    }

    private TeamDocument MapToDocument(Team team)
    {
        return new TeamDocument
        {
            Name = team.Name,
            Category = team.Category,
            Coach = team.Coach,
            LogoUrl = team.LogoUrl,
            StatsTeam = new TeamStatsDocument
            {
                Won = team.StatsTeam.Won,
                Lost = team.StatsTeam.Lost,
                Tied = team.StatsTeam.Tied
            }
        };
    }
}