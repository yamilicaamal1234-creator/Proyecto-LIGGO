using App.Domain.Entities.Sql;
using App.Domain.Enums;
using App.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence.Sql.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _context;

    public SubscriptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetCustomerByExternalIdAsync(string externalId)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.ExternalId == externalId);
    }

    public async Task<Subscription?> GetActiveSubscriptionAsync(int customerId)
    {
        var hoy = DateTime.UtcNow;
        
        return await _context.Subscriptions
            .Include(s => s.Plan)
            .FirstOrDefaultAsync(s => 
                s.IdCustomer == customerId && 
                s.Status == SubscriptionStatus.Active &&
                s.FechaFin > hoy); // ¡La regla de negocio del SaaS!
    }

    public Task UpdateSubscriptionStatusAsync(int subscriptionId, SubscriptionStatus status)
    {
        throw new NotImplementedException();
    }
}