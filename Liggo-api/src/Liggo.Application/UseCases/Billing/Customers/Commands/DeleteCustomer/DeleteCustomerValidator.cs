using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Customers.Commands.DeleteCustomer;

public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es obligatorio para eliminar el Customer.");
    }
}