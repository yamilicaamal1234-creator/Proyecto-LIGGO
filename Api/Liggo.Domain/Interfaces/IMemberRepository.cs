using Liggo.Domain.Entities.Documents;

namespace Liggo.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(string schoolId, string memberId, CancellationToken cancellationToken);
        Task<IEnumerable<Member>> GetAllBySchoolAsync(string schoolId, string? roleFilter, CancellationToken cancellationToken);
        Task AddAsync(string schoolId, Member member, CancellationToken cancellationToken);
        Task UpdatePartialAsync(string schoolId, string memberId, Dictionary<string, object> updates, CancellationToken cancellationToken);
        Task UpdateExternalNameAsync(string schoolId, string memberId, string newName, CancellationToken cancellationToken);
        Task DeleteAsync(string schoolId, string memberId, CancellationToken cancellationToken);
    }
}