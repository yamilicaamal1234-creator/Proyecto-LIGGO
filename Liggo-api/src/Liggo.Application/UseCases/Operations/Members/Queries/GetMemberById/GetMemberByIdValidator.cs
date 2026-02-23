using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Members.Queries.GetMemberById;

public class GetMemberByIdValidator : AbstractValidator<GetMemberByIdQuery>
{
    public GetMemberByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para la búsqueda.");
    }
}