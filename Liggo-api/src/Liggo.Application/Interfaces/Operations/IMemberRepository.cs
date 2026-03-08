using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.Interfaces.Operations;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Member>> GetAllBySchoolIdAsync(Guid schoolId, CancellationToken cancellationToken = default);
    
    Task AddAsync(Member member, CancellationToken cancellationToken = default);
    Task UpdateAsync(Member member, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}