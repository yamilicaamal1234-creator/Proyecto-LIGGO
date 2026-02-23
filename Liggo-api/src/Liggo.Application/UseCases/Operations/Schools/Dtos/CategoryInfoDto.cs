using System.Collections.Generic;

namespace Liggo.Application.UseCases.Operations.Schools.Dtos;

public record CategoryInfoDto(
    string Name, 
    List<int> Years);