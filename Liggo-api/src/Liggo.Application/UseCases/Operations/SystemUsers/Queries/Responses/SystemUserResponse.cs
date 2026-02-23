using System;
using System.Collections.Generic;
using Liggo.Application.UseCases.Operations.SystemUsers.Dtos;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.Responses;

public record SystemUserResponse(
    string Id,
    AuthDataDto Auth,
    GlobalProfileDto GlobalProfile,
    string ActiveTenantId,
    List<string> Tenants,
    DateTime CreatedAt);