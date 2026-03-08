using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Registrations.Commands.CreateRegistration
{
    public class CreateRegistrationHandler : IRequestHandler<CreateRegistrationCommand, Guid>
    {
        private readonly IRegistrationRepository _registrationRepository;

        public CreateRegistrationHandler(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<Guid> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
        {
            var registration = new Registration
            {
                Id = Guid.NewGuid(),
                AdminId = request.AdminId,
                PlayerId = request.PlayerId,
                Plan = request.Plan,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Amount = request.Amount,
                Status = request.Status
            };

            await _registrationRepository.AddAsync(registration);

            return registration.Id;
        }
    }
}
