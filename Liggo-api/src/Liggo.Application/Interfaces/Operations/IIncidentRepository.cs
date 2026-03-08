using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface IIncidentRepository
{
    Task<Incident?> GetByIdAsync(Guid id, Guid adminId);
    Task<IEnumerable<Incident>> GetAllByAdminIdAsync(Guid adminId);
    Task<IEnumerable<Incident>> GetAllByPlayerIdAsync(Guid playerId, Guid adminId);

    Task AddAsync(Incident incident);
    Task UpdateAsync(Incident incident);
    Task DeleteAsync(Guid id, Guid adminId);
}