using System.Threading.Tasks;

namespace Liggo.Application.Interfaces;

public interface IFirestoreTenantRepository
{
    Task CreateTenantStructureAsync(string tenantId, string name);
}