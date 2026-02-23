using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface IIncidentRepository
{
    Task<Incident?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Incident>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Incident>> GetAllByPlayerIdAsync(string playerId, CancellationToken cancellationToken = default);
    
    Task AddAsync(Incident incident, CancellationToken cancellationToken = default);
    Task UpdateAsync(Incident incident, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}