namespace Liggo.Api.Controllers.Operations.Payments.Contracts;

public record UpdatePaymentRequest(
    Guid PlayerId,
    string Concept,
    decimal Amount,
    DateTime Date,
    string Status);