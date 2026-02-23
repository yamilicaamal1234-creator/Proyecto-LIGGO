using FluentValidation;

namespace Liggo.Application.UseCases.Billing.Customers.Commands.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.TenantId).GreaterThan(0).WithMessage("El ID del Tenant es obligatorio.");
        
        RuleFor(x => x.ExternalId)
            .NotEmpty().WithMessage("El ExternalId (Firebase OrgId) es obligatorio para vincular la cuenta.");
            
        RuleFor(x => x.BusinessName)
            .NotEmpty().WithMessage("La razón social o nombre del negocio es obligatorio.");
            
        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("El RFC / Tax ID es obligatorio para facturación.")
            .MinimumLength(12).WithMessage("El RFC debe tener al menos 12 caracteres."); // Asumiendo RFC mexicano
            
        RuleFor(x => x.AdminEmail)
            .NotEmpty().WithMessage("El correo del administrador es obligatorio.")
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.");
    }
}