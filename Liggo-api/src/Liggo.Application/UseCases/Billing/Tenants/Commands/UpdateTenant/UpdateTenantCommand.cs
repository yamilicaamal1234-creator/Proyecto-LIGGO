using MediatR;

public record UpdateTenantCommand(int Id, string Name, string ApiKey, string WebhookUrl) : IRequest<bool>;