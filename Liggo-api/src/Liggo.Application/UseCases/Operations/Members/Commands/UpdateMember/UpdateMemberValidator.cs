using FluentValidation;

namespace Liggo.Application.UseCases.Operations.Members.Commands.UpdateMember;

public class UpdateMemberValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del miembro es obligatorio.");
        RuleFor(x => x.Uid).NotEmpty().WithMessage("El Uid de SystemUser es obligatorio.");
        RuleFor(x => x.Role).Must(x => x == "tutor" || x == "coach" || x == "admin");

        RuleFor(x => x.Profile).NotNull();
        When(x => x.Profile != null, () => RuleFor(x => x.Profile.Name).NotEmpty());

        RuleFor(x => x.Wallet).NotNull();
        When(x => x.Wallet != null, () => 
            RuleFor(x => x.Wallet.Status).Must(x => x == "up_to_date" || x == "overdue"));
    }
}