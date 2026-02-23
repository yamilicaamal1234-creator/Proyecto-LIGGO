using MediatR;

public record CreateTenantCommand(string Name, string ApiKey, string WebhookUrl) : IRequest<int>;