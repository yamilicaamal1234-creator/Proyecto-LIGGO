using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Plans.Commands.DeletePlan;

public class DeletePlanHandler : IRequestHandler<DeletePlanCommand, bool>
{
    private readonly IPlanRepository _planRepository;

    public DeletePlanHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<bool> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (plan == null) return false;

        await _planRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}