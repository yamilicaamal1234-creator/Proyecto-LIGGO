using MediatR;
using Liggo.Domain.Enums;

public record UpdatePlanCommand(
    int Id, 
    string Name, 
    decimal Price, 
    string Currency, 
    PlanFrequency Frequency, 
    bool IsActive) : IRequest<bool>;