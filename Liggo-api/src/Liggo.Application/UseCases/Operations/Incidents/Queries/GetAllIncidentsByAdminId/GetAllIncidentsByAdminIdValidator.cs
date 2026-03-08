using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsByAdminId;

public class GetAllIncidentsByAdminIdValidator : AbstractValidator<GetAllIncidentsByAdminIdQuery>
{
    public GetAllIncidentsByAdminIdValidator()
    {
        RuleFor(x => x.AdminId).NotEmpty().WithMessage("El ID del administrador es obligatorio para listar los incidentes.");
    }
}