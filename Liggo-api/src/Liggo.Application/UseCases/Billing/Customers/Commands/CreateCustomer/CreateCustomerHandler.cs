using MediatR;
using Liggo.Application.Interfaces.Billing;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.UseCases.Billing.Customers.Commands.CreateCustomer;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer 
        { 
            TenantId = request.TenantId,
            ExternalId = request.ExternalId,
            BusinessName = request.BusinessName,
            TaxId = request.TaxId,
            AdminEmail = request.AdminEmail
        };

        await _customerRepository.AddAsync(customer, cancellationToken);

        return customer.Id; 
    }
}   