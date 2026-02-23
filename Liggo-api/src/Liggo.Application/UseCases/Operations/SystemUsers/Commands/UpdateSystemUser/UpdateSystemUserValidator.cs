using FluentValidation;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.UpdateSystemUser;

public class UpdateSystemUserValidator : AbstractValidator<UpdateSystemUserCommand>
{
    public UpdateSystemUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del usuario es obligatorio.");
        
        RuleFor(x => x.Auth).NotNull();
        When(x => x.Auth != null, () => 
        {
            RuleFor(x => x.Auth.Email).NotEmpty().EmailAddress();
        });

        RuleFor(x => x.GlobalProfile).NotNull();
        When(x => x.GlobalProfile != null, () => 
        {
            RuleFor(x => x.GlobalProfile.FullName).NotEmpty();
        });
    }
}