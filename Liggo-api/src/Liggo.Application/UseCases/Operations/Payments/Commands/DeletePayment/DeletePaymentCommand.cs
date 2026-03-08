using MediatR;

namespace Liggo.Application.UseCases.Operations.Payments.Commands.DeletePayment;

public record DeletePaymentCommand(Guid Id, Guid AdminId) : IRequest<bool>;