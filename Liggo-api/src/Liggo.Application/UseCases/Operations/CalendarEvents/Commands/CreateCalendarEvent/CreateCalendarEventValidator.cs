using FluentValidation;

namespace Liggo.Application.UseCases.Operations.CalendarEvents.Commands.CreateCalendarEvent;

public class CreateCalendarEventValidator : AbstractValidator<CreateCalendarEventCommand>
{
    public CreateCalendarEventValidator()
    {
        RuleFor(x => x.Type)
            .Must(x => x == "match" || x == "training")
            .WithMessage("El tipo de evento solo puede ser 'match' o 'training'.");

        RuleFor(x => x.Status)
            .Must(x => x == "scheduled" || x == "finalized" || x == "canceled")
            .WithMessage("El estado debe ser 'scheduled', 'finalized' o 'canceled'.");

        RuleFor(x => x.Metadata).NotNull().WithMessage("Los metadatos son obligatorios.");
        When(x => x.Metadata != null, () =>
        {
            RuleFor(x => x.Metadata.Title).NotEmpty().WithMessage("El título es obligatorio.");
            RuleFor(x => x.Metadata.End)
                .GreaterThan(x => x.Metadata.Start)
                .WithMessage("La fecha de fin debe ser posterior a la de inicio.");
        });

        RuleFor(x => x.Location.Geo)
            .Must(g => g == null || g.Count == 0 || g.Count == 2)
            .WithMessage("Las coordenadas (Geo) deben estar vacías o contener exactamente latitud y longitud [lat, lng].");
    }
}