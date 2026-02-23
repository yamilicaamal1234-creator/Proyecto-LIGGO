using FluentValidation;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Commands.UpdateCalendarEvent;

public class UpdateCalendarEventValidator : AbstractValidator<UpdateCalendarEventCommand>
{
    public UpdateCalendarEventValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del evento es obligatorio.");
        
        RuleFor(x => x.Type).Must(x => x == "match" || x == "training").WithMessage("Tipo inválido.");
        RuleFor(x => x.Status).Must(x => x == "scheduled" || x == "finalized" || x == "canceled").WithMessage("Estado inválido.");
        
        When(x => x.Metadata != null, () =>
        {
            RuleFor(x => x.Metadata.End).GreaterThan(x => x.Metadata.Start).WithMessage("La fecha de fin debe ser posterior a la de inicio.");
        });
    }
}