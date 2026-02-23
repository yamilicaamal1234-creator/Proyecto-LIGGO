using FluentValidation;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetSystemUserByEmail;

public class GetSystemUserByEmailValidator : AbstractValidator<GetSystemUserByEmailQuery>
{
    public GetSystemUserByEmailValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El correo electrónico es obligatorio y debe ser válido.");
    }
}