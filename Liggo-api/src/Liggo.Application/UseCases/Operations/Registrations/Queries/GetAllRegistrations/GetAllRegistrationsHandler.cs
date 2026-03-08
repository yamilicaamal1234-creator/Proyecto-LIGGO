using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Registrations.Dtos;

namespace Liggo.Application.UseCases.Operations.Registrations.Queries.GetAllRegistrations
{
    public class GetAllRegistrationsHandler : IRequestHandler<GetAllRegistrationsQuery, IEnumerable<RegistrationDto>>
    {
        private readonly IRegistrationRepository _registrationRepository;

        public GetAllRegistrationsHandler(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<IEnumerable<RegistrationDto>> Handle(GetAllRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var registrations = await _registrationRepository.GetAllByAdminIdAsync(request.AdminId);

            return registrations.Select(reg => new RegistrationDto
            {
                Id = reg.Id,
                PlayerId = reg.PlayerId,
                PlayerName = reg.Player.FullName, // Assumes Player is included
                Plan = reg.Plan.ToString(),
                StartDate = reg.StartDate,
                EndDate = reg.EndDate,
                Amount = reg.Amount,
                Status = reg.Status.ToString(),
                CreatedAt = reg.CreatedAt
            });
        }
    }
}
