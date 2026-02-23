using MediatR;
using Liggo.Domain.Enums;

// No pedimos IsActive porque por defecto será true al crearse
public record CreatePlanCommand(
    int TenantId, 
    string Name, 
    decimal Price, 
    string Currency, 
    PlanFrequency Frequency) : IRequest<int>;