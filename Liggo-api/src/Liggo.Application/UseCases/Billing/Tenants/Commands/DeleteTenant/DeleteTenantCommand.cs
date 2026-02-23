using MediatR;

public record DeleteTenantCommand(int Id) : IRequest<bool>;