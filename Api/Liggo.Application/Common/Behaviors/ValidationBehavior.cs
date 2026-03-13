using FluentValidation;
using MediatR;

namespace Liggo.Application.Common.Behaviors
{
    // Este pipeline intercepta la petición (TRequest) antes de que llegue al Handler
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> // Asegura que solo aplique a peticiones de MediatR
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        // Inyectamos todos los validadores que existan para este TRequest
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Si hay reglas de validación definidas para esta petición...
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // Ejecuta todas las validaciones al mismo tiempo
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                // Recolecta todos los errores encontrados
                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                // Si hay al menos un error, lanza una excepción (La API devolverá un HTTP 400 Bad Request)
                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            // Si todo está correcto, permite que la petición continúe hacia tu Handler
            return await next();
        }
    }
}