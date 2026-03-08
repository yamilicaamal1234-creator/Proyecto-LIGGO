using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Registrations.Commands.DeleteRegistration;

public class DeleteRegistrationHandler : IRequestHandler<DeleteRegistrationCommand, bool>
{
    private readonly IRegistrationRepository _registrationRepository;

    public DeleteRegistrationHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<bool> Handle(DeleteRegistrationCommand request, CancellationToken cancellationToken)
    {
        var registration = await _registrationRepository.GetByIdAsync(request.Id, request.AdminId);

        if (registration == null) return false;

        await _registrationRepository.DeleteAsync(request.Id, request.AdminId);

        return true;
    }
}