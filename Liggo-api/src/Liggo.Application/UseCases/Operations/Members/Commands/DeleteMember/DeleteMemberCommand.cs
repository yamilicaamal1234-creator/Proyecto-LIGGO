using MediatR;

public record DeleteMemberCommand(string Id) : IRequest<bool>;