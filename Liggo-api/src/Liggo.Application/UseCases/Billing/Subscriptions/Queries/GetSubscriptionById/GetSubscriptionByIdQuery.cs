using System;
using MediatR;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Queries.GetSubscriptionById;

public record SubscriptionResponse(
    int Id,
    int CustomerId,
    int PlanId,
    SubscriptionStatus Status,
    DateTime StartDate,
    DateTime EndDate,
    bool AutoRenew);

public record GetSubscriptionByIdQuery(int Id) : IRequest<SubscriptionResponse?>;