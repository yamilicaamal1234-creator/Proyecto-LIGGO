using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Tenants.Commands.UpdateTenant;

public class UpdateTenantHandler : IRequestHandler<UpdateTenantCommand, bool>
{
    private readonly ITenantRepository _tenantRepository;

    public UpdateTenantHandler(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<bool> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (tenant == null) return false;

        tenant.Name = request.Name;
        tenant.ApiKey = request.ApiKey;
        tenant.WebhookUrl = request.WebhookUrl;

        await _tenantRepository.UpdateAsync(tenant, cancellationToken);
        
        return true;
    }
}