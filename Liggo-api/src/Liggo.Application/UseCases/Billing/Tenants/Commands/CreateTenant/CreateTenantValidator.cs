using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Tenants.Commands.CreateTenant;

public class CreateTenantValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
        RuleFor(x => x.ApiKey).NotEmpty().WithMessage("El ApiKey es obligatorio.");
        // El WebhookUrl podría ser opcional, pero si lo mandan, que sea una URL válida
        RuleFor(x => x.WebhookUrl)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.WebhookUrl))
            .WithMessage("El WebhookUrl debe ser una URL válida.");
    }
}