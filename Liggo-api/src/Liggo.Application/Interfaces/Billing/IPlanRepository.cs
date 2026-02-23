using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.Interfaces.Billing;

public interface IPlanRepository
{
    Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Plan>> GetAllByTenantIdAsync(int tenantId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Plan>> GetAllActiveByTenantIdAsync(int tenantId, CancellationToken cancellationToken = default);
    
    Task AddAsync(Plan plan, CancellationToken cancellationToken = default);
    Task UpdateAsync(Plan plan, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}