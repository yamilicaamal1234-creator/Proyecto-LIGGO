using System;
using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Registrations.Dtos;

namespace Liggo.Application.UseCases.Operations.Registrations.Queries.GetAllRegistrations
{
    public record GetAllRegistrationsQuery(Guid AdminId) : IRequest<IEnumerable<RegistrationDto>>;
}
