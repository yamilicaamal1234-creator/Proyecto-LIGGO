using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Customers.Queries.GetCustomerById;

public class GetCustomerByIdValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es obligatorio para la búsqueda.");
    }
}