using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsBySchoolId;

public class GetAllIncidentsBySchoolIdValidator : AbstractValidator<GetAllIncidentsBySchoolIdQuery>
{
    public GetAllIncidentsBySchoolIdValidator()
    {
        RuleFor(x => x.SchoolId).NotEmpty().WithMessage("El ID de la escuela es obligatorio para listar los incidentes.");
    }
}