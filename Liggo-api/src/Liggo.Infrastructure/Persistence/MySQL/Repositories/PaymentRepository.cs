using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment)
        {
            payment.CreatedAt = DateTime.UtcNow;
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllByAdminIdAsync(Guid adminId)
        {
            return await _context.Payments
                .Where(p => p.AdminId == adminId)
                .Include(p => p.Player)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id, Guid adminId)
        {
            return await _context.Payments
                .Include(p => p.Player)
                .FirstOrDefaultAsync(p => p.Id == id && p.AdminId == adminId);
        }

        public async Task UpdateAsync(Payment payment)
        {
            payment.UpdatedAt = DateTime.UtcNow;
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, Guid adminId)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == id && p.AdminId == adminId);

            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
