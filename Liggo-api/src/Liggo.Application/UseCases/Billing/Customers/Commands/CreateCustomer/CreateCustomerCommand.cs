using MediatR;

public record CreateCustomerCommand(
    int TenantId, 
    string ExternalId, 
    string BusinessName, 
    string TaxId, 
    string AdminEmail) : IRequest<int>;