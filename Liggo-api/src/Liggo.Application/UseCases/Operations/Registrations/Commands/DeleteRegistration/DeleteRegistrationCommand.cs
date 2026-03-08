using MediatR;

namespace Liggo.Application.UseCases.Operations.Registrations.Commands.DeleteRegistration;

public record DeleteRegistrationCommand(Guid Id, Guid AdminId) : IRequest<bool>;