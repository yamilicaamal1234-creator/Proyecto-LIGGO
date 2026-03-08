using FluentValidation;
using System;

namespace Liggo.Application.UseCases.Operations.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El ID del jugador es obligatorio.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento no puede ser en el futuro.");

            RuleFor(x => x.GuardianName)
                .NotEmpty().WithMessage("El nombre del tutor es obligatorio.");

            RuleFor(x => x.GuardianPhone)
                .NotEmpty().WithMessage("El teléfono del tutor es obligatorio.");
        }
    }
}