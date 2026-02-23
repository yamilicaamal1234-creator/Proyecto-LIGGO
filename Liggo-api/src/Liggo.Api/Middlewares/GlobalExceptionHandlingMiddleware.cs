using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Liggo.Application.Exceptions; // Tu excepción personalizada de FluentValidation

namespace Liggo.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Deja que la petición siga su camino hacia los controladores
            await _next(context);
        }
        catch (ValidationException ex)
        {
            // Si FluentValidation detecta un error, lo atrapamos aquí
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new 
            {
                message = "Error de validación en los datos enviados.",
                errors = ex.Errors
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            // Cualquier otro error inesperado (ej. se cayó la base de datos)
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new 
            {
                message = "Ha ocurrido un error interno en el servidor.",
                detail = ex.Message // En producción, podrías ocultar este detalle
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}