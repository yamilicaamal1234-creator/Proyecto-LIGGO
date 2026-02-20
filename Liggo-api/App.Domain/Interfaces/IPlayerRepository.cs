using App.Domain.Entities.NoSql;

namespace App.Domain.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetPlayerByIdAsync(string tenantId, string playerId);
    Task<IEnumerable<Player>> GetPlayersByTeamAsync(string tenantId, string teamId);
    Task CreatePlayerAsync(string tenantId, Player newPlayer);
    Task UpdatePlayerStatsAsync(string tenantId, string playerId, PlayerStats newStats);
}