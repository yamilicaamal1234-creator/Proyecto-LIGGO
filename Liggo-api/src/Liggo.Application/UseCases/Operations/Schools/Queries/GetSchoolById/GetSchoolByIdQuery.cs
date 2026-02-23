using MediatR;
using Liggo.Application.UseCases.Operations.Schools.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Schools.Queries.GetSchoolById;

public record GetSchoolByIdQuery(string Id) : IRequest<SchoolResponse?>;