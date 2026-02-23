using MediatR;

namespace Liggo.Application.UseCases.Billing.Customers.Queries.GetCustomerById;

// El DTO de respuesta que mapearemos a partir de la entidad
public record CustomerResponse(
    int Id, 
    int TenantId, 
    string ExternalId, 
    string BusinessName, 
    string TaxId, 
    string AdminEmail);

public record GetCustomerByIdQuery(int Id) : IRequest<CustomerResponse?>;