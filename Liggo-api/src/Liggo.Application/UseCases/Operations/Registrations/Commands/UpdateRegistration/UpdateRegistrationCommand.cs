using MediatR;

namespace Liggo.Application.UseCases.Operations.Registrations.Commands.UpdateRegistration;

public record UpdateRegistrationCommand(
    Guid Id,
    Guid AdminId,
    string Plan,
    DateTime StartDate,
    DateTime EndDate,
    decimal Amount,
    string Status) : IRequest<bool>;