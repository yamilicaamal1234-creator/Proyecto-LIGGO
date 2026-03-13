using Liggo.Domain.Entities.Relational;

namespace Liggo.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByFirebaseUidAsync(string firebaseUid, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(User user, CancellationToken cancellationToken);

        Task UpdateAsync(User user, CancellationToken cancellationToken);

        Task DeleteAsync(string userId, CancellationToken cancellationToken);
    }
}