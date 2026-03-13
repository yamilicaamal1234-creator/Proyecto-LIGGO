using Liggo.Domain.Entities.Documents;

namespace Liggo.Domain.Interfaces
{
    public interface ITeamRepository
    {
        Task<Team?> GetByIdAsync(string schoolId, string teamId, CancellationToken cancellationToken);

        Task<IEnumerable<Team>> GetAllBySchoolAsync(string schoolId, CancellationToken cancellationToken);

        Task AddAsync(string schoolId, Team team, CancellationToken cancellationToken);

        Task UpdateAsync(string schoolId, Team team, CancellationToken cancellationToken);

        Task DeleteAsync(string schoolId, string teamId, CancellationToken cancellationToken);
    }
}