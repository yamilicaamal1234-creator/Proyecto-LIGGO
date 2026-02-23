using FluentValidation;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.CreateSystemUser;

public class CreateSystemUserValidator : AbstractValidator<CreateSystemUserCommand>
{
    public CreateSystemUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del usuario (UID de Auth) es obligatorio.");
        
        RuleFor(x => x.Auth).NotNull().WithMessage("Los datos de autenticación son obligatorios.");
        When(x => x.Auth != null, () =>
        {
            RuleFor(x => x.Auth.Email).NotEmpty().EmailAddress().WithMessage("El correo electrónico no es válido.");
            RuleFor(x => x.Auth.Provider).NotEmpty().WithMessage("El proveedor es obligatorio (ej. 'email', 'google').");
            RuleFor(x => x.Auth.AccountStatus).NotEmpty().WithMessage("El estado de la cuenta es obligatorio.");
        });

        RuleFor(x => x.GlobalProfile).NotNull().WithMessage("El perfil global es obligatorio.");
        When(x => x.GlobalProfile != null, () =>
        {
            RuleFor(x => x.GlobalProfile.FullName).NotEmpty().WithMessage("El nombre completo es obligatorio.");
        });
    }
}