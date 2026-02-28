using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Billing.Customers.Queries.GetCustomerById;

namespace Liggo.Application.UseCases.Billing.Customers.Queries.GetCustomersByTenantId;

public record GetCustomersByTenantIdQuery(int TenantId) : IRequest<IEnumerable<CustomerResponse>>;
