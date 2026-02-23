using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Tenants.Queries.GetTenantById;

public class GetTenantByIdHandler : IRequestHandler<GetTenantByIdQuery, TenantResponse?>
{
    private readonly ITenantRepository _tenantRepository;

    public GetTenantByIdHandler(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<TenantResponse?> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.Id, cancellationToken);

        if (tenant == null) return null;

        return new TenantResponse(tenant.Id, tenant.Name, tenant.ApiKey, tenant.WebhookUrl);
    }
}