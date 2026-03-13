using Liggo.Domain.Entities.Relational;

namespace Liggo.Domain.Interfaces
{
    public interface ISchoolRepository
    {
        Task<School?> GetByIdAsync(Guid schoolId, CancellationToken cancellationToken);

        Task<IEnumerable<School>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(School school, CancellationToken cancellationToken);

        Task UpdateAsync(School school, CancellationToken cancellationToken);

        Task DeleteAsync(Guid schoolId, CancellationToken cancellationToken);
        Task<bool> HasReachedPlayerLimitAsync(Guid schoolId, int currentPlanLimit, CancellationToken cancellationToken);
    }
}