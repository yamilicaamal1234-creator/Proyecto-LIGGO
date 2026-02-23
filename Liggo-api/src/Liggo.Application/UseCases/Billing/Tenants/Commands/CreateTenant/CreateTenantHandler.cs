using MediatR;
using Liggo.Application.Interfaces.Billing;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.UseCases.Billing.Tenants.Commands.CreateTenant;

public class CreateTenantHandler : IRequestHandler<CreateTenantCommand, int>
{
    private readonly ITenantRepository _tenantRepository;

    public CreateTenantHandler(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<int> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = new Tenant 
        { 
            Name = request.Name, 
            ApiKey = request.ApiKey, 
            WebhookUrl = request.WebhookUrl 
        };

        await _tenantRepository.AddAsync(tenant, cancellationToken);

        return tenant.Id; // EF Core llenará este ID automáticamente al guardar
    }
}