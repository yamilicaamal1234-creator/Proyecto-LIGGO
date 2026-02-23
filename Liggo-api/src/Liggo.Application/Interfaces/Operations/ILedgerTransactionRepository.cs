using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface ILedgerTransactionRepository
{
    Task<LedgerTransaction?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<LedgerTransaction>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default);
    Task<IEnumerable<LedgerTransaction>> GetAllByMemberIdAsync(string memberId, CancellationToken cancellationToken = default);
    
    Task AddAsync(LedgerTransaction transaction, CancellationToken cancellationToken = default);
    Task UpdateAsync(LedgerTransaction transaction, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}