using FluentValidation;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Queries.GetCalendarEventById;

public class GetCalendarEventByIdValidator : AbstractValidator<GetCalendarEventByIdQuery>
{
    public GetCalendarEventByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para la búsqueda.");
    }
}