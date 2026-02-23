using System;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Dtos;

public record AuthDataDto(
    string Email, 
    string Provider, 
    bool EmailVerified, 
    string AccountStatus, 
    DateTime LastLogin);