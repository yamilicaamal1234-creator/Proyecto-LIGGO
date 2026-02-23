using System;
using MediatR;
using Liggo.Domain.Enums;

public record UpdateSubscriptionCommand(
    int Id,
    int CustomerId,
    int PlanId,
    SubscriptionStatus Status,
    DateTime StartDate,
    DateTime EndDate,
    bool AutoRenew) : IRequest<bool>;