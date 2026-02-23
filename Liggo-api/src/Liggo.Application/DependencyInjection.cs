using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Liggo.Application.Behaviors;

namespace Liggo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // 1. Registra automáticamente todos los validadores (las clases AbstractValidator)
        services.AddValidatorsFromAssembly(assembly);

        // 2. Registra MediatR y todos los Handlers
        services.AddMediatR(configuration => 
        {
            configuration.RegisterServicesFromAssembly(assembly);
            
            // 3. Registra el comportamiento del guardia de seguridad (ValidationBehavior)
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}