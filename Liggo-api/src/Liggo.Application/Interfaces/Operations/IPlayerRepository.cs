using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Player>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Player>> GetAllByTeamIdAsync(string teamId, CancellationToken cancellationToken = default);
    
    Task AddAsync(Player player, CancellationToken cancellationToken = default);
    Task UpdateAsync(Player player, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}