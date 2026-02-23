using MediatR;
using Liggo.Application.UseCases.Operations.Schools.Dtos;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.UpdateSchool;

public record UpdateSchoolCommand(
    string Id,
    SchoolInfoDto Info,
    SchoolSettingsDto Settings) : IRequest<bool>;