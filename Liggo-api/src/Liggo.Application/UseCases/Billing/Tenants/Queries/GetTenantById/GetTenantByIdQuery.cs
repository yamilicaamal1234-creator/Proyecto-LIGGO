using MediatR;

namespace Liggo.Application.UseCases.Billing.Tenants.Queries.GetTenantById;

public record TenantResponse(int Id, string Name, string ApiKey, string WebhookUrl);

public record GetTenantByIdQuery(int Id) : IRequest<TenantResponse?>;