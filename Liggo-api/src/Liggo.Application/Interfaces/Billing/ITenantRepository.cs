using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.Interfaces.Billing;

public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Tenant?> GetByApiKeyAsync(string apiKey, CancellationToken cancellationToken = default); 
    Task<IEnumerable<Tenant>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task AddAsync(Tenant tenant, CancellationToken cancellationToken = default);
    Task UpdateAsync(Tenant tenant, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}