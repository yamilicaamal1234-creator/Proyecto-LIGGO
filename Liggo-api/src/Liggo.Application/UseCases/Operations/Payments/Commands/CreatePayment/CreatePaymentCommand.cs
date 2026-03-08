using System;
using MediatR;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Payments.Commands.CreatePayment
{
    public record CreatePaymentCommand(
        Guid AdminId,
        Guid PlayerId,
        string Concept,
        decimal Amount,
        DateTime Date,
        PaymentStatus Status) : IRequest<Guid>;
}
