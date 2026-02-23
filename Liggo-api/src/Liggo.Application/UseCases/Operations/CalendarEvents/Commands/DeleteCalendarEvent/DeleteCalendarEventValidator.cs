using FluentValidation;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Commands.DeleteCalendarEvent;

public class DeleteCalendarEventValidator : AbstractValidator<DeleteCalendarEventCommand>
{
    public DeleteCalendarEventValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para eliminar el Evento.");
    }
}