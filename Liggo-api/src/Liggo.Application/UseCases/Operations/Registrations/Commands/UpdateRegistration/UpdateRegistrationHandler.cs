using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Registrations.Commands.UpdateRegistration;

public class UpdateRegistrationHandler : IRequestHandler<UpdateRegistrationCommand, bool>
{
    private readonly IRegistrationRepository _registrationRepository;

    public UpdateRegistrationHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<bool> Handle(UpdateRegistrationCommand request, CancellationToken cancellationToken)
    {
        var registration = await _registrationRepository.GetByIdAsync(request.Id, request.AdminId);

        if (registration == null) return false;

        registration.Plan = Enum.Parse<PaymentPlan>(request.Plan);
        registration.StartDate = request.StartDate;
        registration.EndDate = request.EndDate;
        registration.Amount = request.Amount;
        registration.Status = Enum.Parse<RegistrationStatus>(request.Status);

        await _registrationRepository.UpdateAsync(registration);

        return true;
    }
}