using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Liggo.Application.Interfaces.Billing;
using Liggo.Application.Interfaces.Operations; // <- Faltaba esto
using Liggo.Infrastructure.Persistence.MySQL;
using Liggo.Infrastructure.Persistence.MySQL.Repositories;
using Liggo.Infrastructure.Persistence.Firebase; // <- Faltaba esto
using Liggo.Infrastructure.Persistence.Firebase.Repositories; // <- Faltaba esto

namespace Liggo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // ==========================================
        // 1. CONFIGURACIÓN DE MYSQL (MÓDULO BILLING)
        // ==========================================
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        // ==========================================
        // 2. CONFIGURACIÓN DE FIREBASE (OPERATIONS)
        // ==========================================
        services.AddSingleton<FirestoreProvider>();

        services.AddScoped<ISchoolRepository, SchoolRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ILedgerTransactionRepository, LedgerTransactionRepository>();
        services.AddScoped<ISystemUserRepository, SystemUserRepository>();
        services.AddScoped<IIncidentRepository, IncidentRepository>();
        services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();

        return services; 
    }
}