using System;
using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.SystemUsers.Dtos;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.UpdateSystemUser;

public record UpdateSystemUserCommand(
    string Id,
    AuthDataDto Auth,
    GlobalProfileDto GlobalProfile,
    string ActiveTenantId,
    List<string> Tenants) : IRequest<bool>;