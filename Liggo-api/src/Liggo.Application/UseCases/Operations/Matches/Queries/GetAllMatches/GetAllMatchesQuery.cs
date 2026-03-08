using System;
using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Matches.Dtos;

namespace Liggo.Application.UseCases.Operations.Matches.Queries.GetAllMatches
{
    public record GetAllMatchesQuery(Guid AdminId) : IRequest<IEnumerable<MatchDto>>;
}
