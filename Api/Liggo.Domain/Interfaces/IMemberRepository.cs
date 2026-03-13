using Liggo.Domain.Entities.Documents;

namespace Liggo.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(string schoolId, string memberId, CancellationToken cancellationToken);

        Task<IEnumerable<Member>> GetAllBySchoolAsync(string schoolId, CancellationToken cancellationToken);

        Task AddAsync(string schoolId, Member member, CancellationToken cancellationToken);

        Task UpdateAsync(string schoolId, Member member, CancellationToken cancellationToken);

        Task DeleteAsync(string schoolId, string memberId, CancellationToken cancellationToken);
    }
}