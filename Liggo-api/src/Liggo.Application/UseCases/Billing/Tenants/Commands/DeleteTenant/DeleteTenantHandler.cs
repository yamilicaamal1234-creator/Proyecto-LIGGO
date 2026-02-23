using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Tenants.Commands.DeleteTenant;

public class DeleteTenantHandler : IRequestHandler<DeleteTenantCommand, bool>
{
    private readonly ITenantRepository _tenantRepository;

    public DeleteTenantHandler(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<bool> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (tenant == null) return false;

        await _tenantRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}