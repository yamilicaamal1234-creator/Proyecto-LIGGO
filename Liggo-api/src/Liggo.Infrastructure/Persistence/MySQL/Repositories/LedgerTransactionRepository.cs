using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;
using Liggo.Infrastructure.Persistence.MySQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liggo.Infrastructure.Persistence.MySQL.Repositories
{
    public class LedgerTransactionRepository : ILedgerTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public LedgerTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LedgerTransaction?> GetByIdAsync(Guid id, Guid adminId)
        {
            return await _context.LedgerTransactions
                .FirstOrDefaultAsync(t => t.Id == id && t.AdminId == adminId);
        }

        public async Task<IEnumerable<LedgerTransaction>> GetAllBySchoolIdAsync(Guid schoolId, Guid adminId)
        {
            return await _context.LedgerTransactions
                .Where(t => t.SchoolId == schoolId && t.AdminId == adminId)
                .ToListAsync();
        }

        public async Task<IEnumerable<LedgerTransaction>> GetAllByMemberIdAsync(Guid memberId, Guid adminId)
        {
            return await _context.LedgerTransactions
                .Where(t => t.MemberId == memberId && t.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddAsync(LedgerTransaction transaction)
        {
            transaction.CreatedAt = DateTime.UtcNow;
            await _context.LedgerTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LedgerTransaction transaction)
        {
            var existingTransaction = await _context.LedgerTransactions
                .FirstOrDefaultAsync(t => t.Id == transaction.Id && t.AdminId == transaction.AdminId);

            if (existingTransaction != null)
            {
                existingTransaction.UpdatedAt = DateTime.UtcNow;
                existingTransaction.Type = transaction.Type;
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.Concept = transaction.Concept;
                existingTransaction.Method = transaction.Method;
                existingTransaction.TransactionRef = transaction.TransactionRef;
                existingTransaction.PayerName = transaction.PayerName;
                existingTransaction.StudentName = transaction.StudentName;
                
                _context.LedgerTransactions.Update(existingTransaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id, Guid adminId)
        {
            var transaction = await _context.LedgerTransactions
                .FirstOrDefaultAsync(t => t.Id == id && t.AdminId == adminId);
            if (transaction != null)
            {
                _context.LedgerTransactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}