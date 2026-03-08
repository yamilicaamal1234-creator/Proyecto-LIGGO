using Liggo.Domain.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liggo.Application.Interfaces.Operations
{
    public interface ILedgerTransactionRepository
    {
        Task<LedgerTransaction?> GetByIdAsync(Guid id, Guid adminId);
        Task<IEnumerable<LedgerTransaction>> GetAllBySchoolIdAsync(Guid schoolId, Guid adminId);
        Task<IEnumerable<LedgerTransaction>> GetAllByMemberIdAsync(Guid memberId, Guid adminId);
        Task AddAsync(LedgerTransaction transaction);
        Task UpdateAsync(LedgerTransaction transaction);
        Task DeleteAsync(Guid id, Guid adminId);
    }
}
