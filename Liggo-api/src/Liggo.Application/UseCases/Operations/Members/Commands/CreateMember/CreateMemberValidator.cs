using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Members.Commands.CreateMember;

public class CreateMemberValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberValidator()
    {
        RuleFor(x => x.Uid).NotEmpty().WithMessage("El Uid de SystemUser es obligatorio para vincular la cuenta.");
        
        RuleFor(x => x.Role)
            .Must(x => x == "tutor" || x == "coach" || x == "admin")
            .WithMessage("El rol debe ser 'tutor', 'coach' o 'admin'.");

        RuleFor(x => x.Profile).NotNull().WithMessage("El perfil es obligatorio.");
        When(x => x.Profile != null, () =>
        {
            RuleFor(x => x.Profile.Name).NotEmpty().WithMessage("El nombre del miembro es obligatorio.");
            // El teléfono podría ser opcional, si no lo es, agrega: RuleFor(x => x.Profile.Phone).NotEmpty();
        });

        RuleFor(x => x.Wallet).NotNull().WithMessage("La información de la billetera es obligatoria.");
        When(x => x.Wallet != null, () =>
        {
            RuleFor(x => x.Wallet.Status)
                .Must(x => x == "up_to_date" || x == "overdue")
                .WithMessage("El estado de la billetera debe ser 'up_to_date' o 'overdue'.");
        });
    }
}