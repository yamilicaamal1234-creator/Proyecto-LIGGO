using Liggo.Domain.Entities.Documents;

namespace Liggo.Domain.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident?> GetByIdAsync(string schoolId, string incidentId, CancellationToken cancellationToken);
        Task<IEnumerable<Incident>> GetAllBySchoolAsync(string schoolId, string? type, CancellationToken cancellationToken);
        Task AddAsync(string schoolId, Incident incident, CancellationToken cancellationToken);
        Task UpdatePartialAsync(string schoolId, string incidentId, Dictionary<string, object> updates, CancellationToken cancellationToken);
        Task DeleteAsync(string schoolId, string incidentId, CancellationToken cancellationToken);
    }
}