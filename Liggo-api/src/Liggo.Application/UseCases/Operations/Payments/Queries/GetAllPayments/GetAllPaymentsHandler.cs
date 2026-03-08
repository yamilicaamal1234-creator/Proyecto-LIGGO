using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Payments.Dtos;

namespace Liggo.Application.UseCases.Operations.Payments.Queries.GetAllPayments
{
    public class GetAllPaymentsHandler : IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentDto>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetAllPaymentsHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAllByAdminIdAsync(request.AdminId);

            return payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                PlayerId = p.PlayerId,
                PlayerName = p.Player.FullName,
                Concept = p.Concept,
                Amount = p.Amount,
                Date = p.Date,
                Status = p.Status.ToString()
            });
        }
    }
}
