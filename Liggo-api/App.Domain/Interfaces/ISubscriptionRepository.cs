using App.Domain.Entities.Sql;
using App.Domain.Enums;

namespace App.Domain.Interfaces;

public interface ISubscriptionRepository
{
    // Busca al cliente usando el orgId de Firebase
    Task<Customer?> GetCustomerByExternalIdAsync(string externalId);
    
    // Verifica si el cliente tiene una suscripción vigente
    Task<Subscription?> GetActiveSubscriptionAsync(int customerId);
    
    // Actualiza el estado cuando entra un pago
    Task UpdateSubscriptionStatusAsync(int subscriptionId, SubscriptionStatus status);
}