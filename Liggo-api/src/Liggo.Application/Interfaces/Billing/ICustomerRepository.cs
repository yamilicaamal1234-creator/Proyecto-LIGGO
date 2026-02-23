using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.Interfaces.Billing;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Customer>> GetAllByTenantIdAsync(int tenantId, CancellationToken cancellationToken = default);
    // CRÍTICO: Este método te servirá para buscar un cliente de MySQL usando el ID de Firebase
    Task<Customer?> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken = default);
    
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}