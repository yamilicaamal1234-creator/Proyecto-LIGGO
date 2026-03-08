using System;
using MediatR;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Registrations.Commands.CreateRegistration
{
    public record CreateRegistrationCommand(
        Guid AdminId,
        Guid PlayerId,
        PaymentPlan Plan,
        DateTime StartDate,
        DateTime EndDate,
        decimal Amount,
        RegistrationStatus Status) : IRequest<Guid>;
}
