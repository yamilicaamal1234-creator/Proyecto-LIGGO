using MediatR;

public record UpdateCustomerCommand(
    int Id, 
    int TenantId, 
    string ExternalId, 
    string BusinessName, 
    string TaxId, 
    string AdminEmail) : IRequest<bool>;