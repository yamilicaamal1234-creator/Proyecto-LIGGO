using System;
using MediatR;
using Liggo.Application.UseCases.Operations.Matches.Dtos;

namespace Liggo.Application.UseCases.Operations.Matches.Queries.GetMatchById
{
    public record GetMatchByIdQuery(Guid Id, Guid AdminId) : IRequest<MatchDto>;
}
