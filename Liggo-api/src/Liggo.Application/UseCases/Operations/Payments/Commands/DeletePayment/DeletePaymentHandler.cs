using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Payments.Commands.DeletePayment;

public class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand, bool>
{
    private readonly IPaymentRepository _paymentRepository;

    public DeletePaymentHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(request.Id, request.AdminId);

        if (payment == null) return false;

        await _paymentRepository.DeleteAsync(request.Id, request.AdminId);

        return true;
    }
}