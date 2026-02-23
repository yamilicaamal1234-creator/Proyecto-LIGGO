using MediatR;

public record DeleteSubscriptionCommand(int Id) : IRequest<bool>;