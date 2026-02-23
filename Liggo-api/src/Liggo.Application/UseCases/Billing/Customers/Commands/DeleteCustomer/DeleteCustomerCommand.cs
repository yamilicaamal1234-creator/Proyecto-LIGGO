using MediatR;

public record DeleteCustomerCommand(int Id) : IRequest<bool>;