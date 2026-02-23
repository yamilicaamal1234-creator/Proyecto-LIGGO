using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Infrastructure.Persistence.Firebase.Repositories;

public class IncidentRepository : IIncidentRepository
{
    public Task<Incident?> GetByIdAsync(string id, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task<IEnumerable<Incident>> GetAllBySchoolIdAsync(string schoolId, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task<IEnumerable<Incident>> GetAllByPlayerIdAsync(string playerId, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task AddAsync(Incident incident, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task UpdateAsync(Incident incident, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
    public Task DeleteAsync(string id, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();
}