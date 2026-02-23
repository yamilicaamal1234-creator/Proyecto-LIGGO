using System;
using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.SystemUsers.Dtos;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.CreateSystemUser;

public record CreateSystemUserCommand(
    string Id, // Recibimos el UID de Firebase Auth
    AuthDataDto Auth,
    GlobalProfileDto GlobalProfile,
    string ActiveTenantId,
    List<string> Tenants,
    DateTime CreatedAt) : IRequest<string>;