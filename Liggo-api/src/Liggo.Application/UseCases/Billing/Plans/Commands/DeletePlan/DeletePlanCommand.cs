using MediatR;

public record DeletePlanCommand(int Id) : IRequest<bool>;