using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Payments.Commands.CreatePayment
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                AdminId = request.AdminId,
                PlayerId = request.PlayerId,
                Concept = request.Concept,
                Amount = request.Amount,
                Date = request.Date,
                Status = request.Status
            };

            await _paymentRepository.AddAsync(payment);

            return payment.Id;
        }
    }
}
