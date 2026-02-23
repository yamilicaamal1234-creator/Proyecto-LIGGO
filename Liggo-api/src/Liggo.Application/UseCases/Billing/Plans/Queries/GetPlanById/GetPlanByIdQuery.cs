using MediatR;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Billing.Plans.Queries.GetPlanById;

public record PlanResponse(
    int Id, 
    int TenantId, 
    string Name, 
    decimal Price, 
    string Currency, 
    PlanFrequency Frequency, 
    bool IsActive);

public record GetPlanByIdQuery(int Id) : IRequest<PlanResponse?>;