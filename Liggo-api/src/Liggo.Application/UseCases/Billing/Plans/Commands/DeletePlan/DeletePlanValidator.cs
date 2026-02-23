using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Plans.Commands.DeletePlan;

public class DeletePlanValidator : AbstractValidator<DeletePlanCommand>
{
    public DeletePlanValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es obligatorio para eliminar el Plan.");
    }
}