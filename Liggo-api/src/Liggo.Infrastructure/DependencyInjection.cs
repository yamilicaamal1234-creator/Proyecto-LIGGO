using Liggo.Application.Interfaces;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.Interfaces.Billing;
using Liggo.Infrastructure.Persistence.MySQL.Repositories;
using Liggo.Infrastructure.Persistence.MySQL;
using Liggo.Infrastructure.Services;
using Liggo.Infrastructure.Persistence.Firebase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Liggo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 21))));

        // MySQL Repositories
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IIncidentRepository, IncidentRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();

        // Dependencia principal de Firebase
        services.AddSingleton<Liggo.Infrastructure.Persistence.Firebase.FirestoreProvider>();

        // Repositorios que faltaban en el registro
        services.AddScoped<ISystemUserRepository, Liggo.Infrastructure.Persistence.Firebase.Repositories.SystemUserRepository>();
        services.AddScoped<ISchoolRepository, Liggo.Infrastructure.Persistence.Firebase.Repositories.SchoolRepository>();
        services.AddScoped<ITeamRepository, Liggo.Infrastructure.Persistence.Firebase.Repositories.TeamRepository>();
        services.AddScoped<IMemberRepository, Liggo.Infrastructure.Persistence.Firebase.Repositories.MemberRepository>();
        services.AddScoped<ILedgerTransactionRepository, Liggo.Infrastructure.Persistence.Firebase.Repositories.LedgerTransactionRepository>();
        services.AddScoped<ICalendarEventRepository, Liggo.Infrastructure.Persistence.Firebase.Repositories.CalendarEventRepository>();

        return services;
    }
}