using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Plans.Queries.GetPlanById;

public class GetPlanByIdHandler : IRequestHandler<GetPlanByIdQuery, PlanResponse?>
{
    private readonly IPlanRepository _planRepository;

    public GetPlanByIdHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<PlanResponse?> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);

        if (plan == null) return null;

        return new PlanResponse(
            plan.Id, 
            plan.TenantId, 
            plan.Name, 
            plan.Price, 
            plan.Currency, 
            plan.Frequency, 
            plan.IsActive);
    }
}