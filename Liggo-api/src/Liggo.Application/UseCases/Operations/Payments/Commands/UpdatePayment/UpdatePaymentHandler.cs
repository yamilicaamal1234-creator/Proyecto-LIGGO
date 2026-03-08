using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Payments.Commands.UpdatePayment;

public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand, bool>
{
    private readonly IPaymentRepository _paymentRepository;

    public UpdatePaymentHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<bool> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(request.Id, request.AdminId);

        if (payment == null) return false;

        payment.PlayerId = request.PlayerId;
        payment.Concept = request.Concept;
        payment.Amount = request.Amount;
        payment.Date = request.Date;
        payment.Status = Enum.Parse<PaymentStatus>(request.Status);

        await _paymentRepository.UpdateAsync(payment);

        return true;
    }
}