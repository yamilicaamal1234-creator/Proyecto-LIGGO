using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface ISchoolRepository
{
    Task<School?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    // Buscamos las escuelas que pertenecen a un Tenant (SaaS)
    Task<IEnumerable<School>> GetAllByTenantIdAsync(string tenantId, CancellationToken cancellationToken = default);
    
    Task AddAsync(School school, CancellationToken cancellationToken = default);
    Task UpdateAsync(School school, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}