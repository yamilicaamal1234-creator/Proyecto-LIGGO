using Liggo.Domain.Entities.Documents;

namespace Liggo.Domain.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident?> GetByIdAsync(string schoolId, string incidentId, CancellationToken cancellationToken);
        Task<IEnumerable<Incident>> GetAllBySchoolAsync(string schoolId, CancellationToken cancellationToken);
        Task AddAsync(string schoolId, Incident incident, CancellationToken cancellationToken);
        Task UpdateAsync(string schoolId, Incident incident, CancellationToken cancellationToken);
        Task DeleteAsync(string schoolId, string incidentId, CancellationToken cancellationToken);
    }
}