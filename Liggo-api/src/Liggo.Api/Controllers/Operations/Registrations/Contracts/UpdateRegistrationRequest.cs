namespace Liggo.Api.Controllers.Operations.Registrations.Contracts;

public record UpdateRegistrationRequest(
    string Plan,
    DateTime StartDate,
    DateTime EndDate,
    decimal Amount,
    string Status);