using System.Collections.Generic;

namespace Liggo.Application.UseCases.Operations.Schools.Dtos;

public record SchoolSettingsDto(
    string Currency, 
    string Timezone, 
    List<CategoryInfoDto> Categories);