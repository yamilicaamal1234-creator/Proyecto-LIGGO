using MediatR;

namespace Liggo.Application.UseCases.Operations.Payments.Commands.UpdatePayment;

public record UpdatePaymentCommand(
    Guid Id,
    Guid AdminId,
    Guid PlayerId,
    string Concept,
    decimal Amount,
    DateTime Date,
    string Status) : IRequest<bool>;