using FluentValidation;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetSystemUserById;

public class GetSystemUserByIdValidator : AbstractValidator<GetSystemUserByIdQuery>
{
    public GetSystemUserByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio.");
    }
}