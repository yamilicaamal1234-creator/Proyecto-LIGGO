using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Customers.Commands.UpdateCustomer;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (customer == null) return false;

        customer.TenantId = request.TenantId;
        customer.ExternalId = request.ExternalId;
        customer.BusinessName = request.BusinessName;
        customer.TaxId = request.TaxId;
        customer.AdminEmail = request.AdminEmail;

        await _customerRepository.UpdateAsync(customer, cancellationToken);
        
        return true;
    }
}