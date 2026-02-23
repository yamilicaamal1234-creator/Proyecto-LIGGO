using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Plans.Commands.UpdatePlan;

public class UpdatePlanHandler : IRequestHandler<UpdatePlanCommand, bool>
{
    private readonly IPlanRepository _planRepository;

    public UpdatePlanHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<bool> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (plan == null) return false;

        plan.Name = request.Name;
        plan.Price = request.Price;
        plan.Currency = request.Currency.ToUpper();
        plan.Frequency = request.Frequency;
        plan.IsActive = request.IsActive;

        await _planRepository.UpdateAsync(plan, cancellationToken);
        
        return true;
    }
}