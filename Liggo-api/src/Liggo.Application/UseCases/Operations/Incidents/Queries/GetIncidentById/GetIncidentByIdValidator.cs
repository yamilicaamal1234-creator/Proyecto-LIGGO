using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById;

public class GetIncidentByIdValidator : AbstractValidator<GetIncidentByIdQuery>
{
    public GetIncidentByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para la búsqueda.");
    }
}