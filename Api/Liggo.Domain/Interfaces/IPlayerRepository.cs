using Liggo.Domain.Entities.Documents;

namespace Liggo.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player?> GetByIdAsync(string schoolId, string playerId, CancellationToken cancellationToken);

        Task<IEnumerable<Player>> GetAllBySchoolAsync(string schoolId, CancellationToken cancellationToken);

        Task AddAsync(string schoolId, Player player, CancellationToken cancellationToken);

        Task UpdateAsync(string schoolId, Player player, CancellationToken cancellationToken);

        Task DeleteAsync(string schoolId, string playerId, CancellationToken cancellationToken);
    }
}