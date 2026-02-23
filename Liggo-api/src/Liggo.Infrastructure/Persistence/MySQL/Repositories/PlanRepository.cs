using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Liggo.Application.Interfaces.Billing;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly ApplicationDbContext _context;

    public PlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Plans.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Plan>> GetAllByTenantIdAsync(int tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.Plans
            .Where(p => p.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }

    // Filtra para traer solo los planes que no han sido dados de baja
    public async Task<IEnumerable<Plan>> GetAllActiveByTenantIdAsync(int tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.Plans
            .Where(p => p.TenantId == tenantId && p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Plan plan, CancellationToken cancellationToken = default)
    {
        await _context.Plans.AddAsync(plan, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Plan plan, CancellationToken cancellationToken = default)
    {
        _context.Plans.Update(plan);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var plan = await GetByIdAsync(id, cancellationToken);
        if (plan != null)
        {
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}