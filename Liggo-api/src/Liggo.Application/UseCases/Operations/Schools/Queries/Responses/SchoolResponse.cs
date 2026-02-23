using Liggo.Application.UseCases.Operations.Schools.Dtos;

namespace Liggo.Application.UseCases.Operations.Schools.Queries.Responses;

public record SchoolResponse(
    string Id,
    SchoolInfoDto Info,
    SchoolSettingsDto Settings);