using Liggo.Domain.Entities.Relational;

namespace Liggo.Domain.Interfaces
{
    public interface ILedgerTransactionRepository
    {
        Task<LedgerTransaction?> GetByIdAsync(string transactionId, CancellationToken cancellationToken);

        Task<IEnumerable<LedgerTransaction>> GetAllBySchoolAsync(Guid schoolId, CancellationToken cancellationToken);

        Task AddAsync(LedgerTransaction transaction, CancellationToken cancellationToken);

        Task UpdateAsync(LedgerTransaction transaction, CancellationToken cancellationToken);

        Task DeleteAsync(string transactionId, CancellationToken cancellationToken);
    }
}