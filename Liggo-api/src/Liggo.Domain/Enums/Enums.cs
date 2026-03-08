namespace Liggo.Domain.Enums;

public enum SubscriptionStatus
{
    Active = 1,
    PastDue = 2,
    Canceled = 3
}

public enum PlanFrequency
{
    Mensual = 1,
    Anual = 2,
    Unico = 3
}

public enum PaymentPlan
{
    Anual,
    Semestral
}

public enum RegistrationStatus
{
    Pending,
    Active,
    Expired
}

public enum MatchCategory
{
    Sub9,
    Sub11,
    Sub13,
    Sub15,
    Sub17
}

public enum AttendanceStatus
{
    Present = 41,
    Absent = 42,
    Justified = 43,
    Tardy = 44
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Overdue,
    Canceled
}

public enum IncidentType
{
    Injury,
    Sanction,
    Administrative
}

public enum IncidentStatus
{
    Active,
    Resolved
}