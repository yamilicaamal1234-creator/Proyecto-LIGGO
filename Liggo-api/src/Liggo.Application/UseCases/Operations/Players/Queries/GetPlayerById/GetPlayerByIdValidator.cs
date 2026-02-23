using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetPlayerById;

public class GetPlayerByIdValidator : AbstractValidator<GetPlayerByIdQuery>
{
    public GetPlayerByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio.");
    }
}