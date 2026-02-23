using MediatR;
using Liggo.Application.Interfaces.Billing;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.UseCases.Billing.Plans.Commands.CreatePlan;

public class CreatePlanHandler : IRequestHandler<CreatePlanCommand, int>
{
    private readonly IPlanRepository _planRepository;

    public CreatePlanHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<int> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = new Plan 
        { 
            TenantId = request.TenantId,
            Name = request.Name,
            Price = request.Price,
            Currency = string.IsNullOrWhiteSpace(request.Currency) ? "MXN" : request.Currency.ToUpper(),
            Frequency = request.Frequency,
            IsActive = true // Valor por defecto según tu dominio
        };

        await _planRepository.AddAsync(plan, cancellationToken);

        return plan.Id; 
    }
}