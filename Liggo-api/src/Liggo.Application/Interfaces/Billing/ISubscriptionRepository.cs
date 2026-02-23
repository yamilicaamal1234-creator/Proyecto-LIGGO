using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.Interfaces.Billing;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Subscription>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
    // Para validar si un cliente ya tiene una suscripción activa
    Task<Subscription?> GetActiveSubscriptionByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
    
    Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default);
    Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}