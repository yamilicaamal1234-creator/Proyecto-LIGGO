using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Billing;
using Liggo.Application.UseCases.Billing.Customers.Queries.GetCustomerById;

namespace Liggo.Application.UseCases.Billing.Customers.Queries.GetCustomersByTenantId;

public class GetCustomersByTenantIdHandler : IRequestHandler<GetCustomersByTenantIdQuery, IEnumerable<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersByTenantIdHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerResponse>> Handle(GetCustomersByTenantIdQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllByTenantIdAsync(request.TenantId, cancellationToken);

        return customers.Select(customer => new CustomerResponse(
            customer.Id,
            customer.TenantId,
            customer.ExternalId,
            customer.BusinessName,
            customer.TaxId,
            customer.AdminEmail
        ));
    }
}
