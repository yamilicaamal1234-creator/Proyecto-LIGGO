using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Team>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default);
    
    Task AddAsync(Team team, CancellationToken cancellationToken = default);
    Task UpdateAsync(Team team, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}