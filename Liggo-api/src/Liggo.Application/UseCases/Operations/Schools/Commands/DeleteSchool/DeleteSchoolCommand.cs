using MediatR;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.DeleteSchool;

public record DeleteSchoolCommand(string Id) : IRequest<bool>;