using System;
using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Payments.Dtos;

namespace Liggo.Application.UseCases.Operations.Payments.Queries.GetAllPayments
{
    public record GetAllPaymentsQuery(Guid AdminId) : IRequest<IEnumerable<PaymentDto>>;
}
