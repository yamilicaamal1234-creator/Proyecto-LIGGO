using FluentValidation;
using MediatR;
using Liggo.Domain.Entities.Relational;
using Liggo.Domain.Interfaces;

namespace Liggo.Application.Functions.Schools.Commands
{
    // ==========================================================
    // 1. EL COMMAND (Los datos de entrada desde Angular)
    // ==========================================================
    public class CreateSchoolCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public int PlanId { get; set; }
        public string Currency { get; set; } = "MXN";
        public string TimeZone { get; set; } = "America/Mexico_City";
        public string AdminEmail { get; set; } = string.Empty;
        public string AdminFirebaseUid { get; set; } = string.Empty;
    }

    // ==========================================================
    // 2. EL VALIDADOR (El Filtro de Seguridad)
    // ==========================================================
    public class CreateSchoolCommandValidator : AbstractValidator<CreateSchoolCommand>
    {
        public CreateSchoolCommandValidator()
        {
            // Gracias a FluentValidation, evitamos los "if" feos
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la escuela es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.PlanId)
                .GreaterThan(0).WithMessage("Debe seleccionar un plan válido.");

            RuleFor(x => x.Currency)
                .Length(3).WithMessage("La moneda debe tener exactamente 3 letras (ej. MXN).");

            RuleFor(x => x.AdminEmail)
                .NotEmpty()
                .EmailAddress().WithMessage("Debe proporcionar un email válido para el administrador.");

            RuleFor(x => x.AdminFirebaseUid)
                .NotEmpty().WithMessage("El UID de Firebase es obligatorio para vincular la cuenta.");
        }
    }

    // ==========================================================
    // 3. EL HANDLER (La Lógica de Ejecución)
    // ==========================================================
    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseService _firebaseService;

        // Inyectamos las interfaces que creamos en el Domain
        public CreateSchoolCommandHandler(
            IApplicationDbContext context, 
            IUnitOfWork unitOfWork, 
            IFirebaseService firebaseService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _firebaseService = firebaseService;
        }

        public async Task<Guid> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            // 1. Iniciamos la transacción segura
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                // 2. Preparamos la entidad School (MySQL)
                var school = new School
                {
                    Name = request.Name,
                    PlanId = request.PlanId,
                    Currency = request.Currency,
                    TimeZone = request.TimeZone,
                    SubscriptionStatus = SubscriptionStatus.Active // El Tenant nace activo
                };
                
                // Lo agregamos a la memoria del Entity Framework
                await _context.Schools.AddAsync(school, cancellationToken);

                // 3. Preparamos al Usuario Dueño (MySQL)
                var admin = new User
                {
                    FirebaseUid = request.AdminFirebaseUid,
                    Email = request.AdminEmail,
                    SchoolId = school.Id, // Vinculamos al admin con la escuela recién creada
                    IsSuperAdmin = true
                };

                await _context.Users.AddAsync(admin, cancellationToken);

                // 4. Guardamos en MySQL (Aún no es definitivo hasta el Commit)
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // 5. Intentamos crear la escuela en Firebase
                await _firebaseService.CreateSchoolDocumentAsync(
                    school.Id.ToString(), 
                    school.Name, 
                    school.PlanId, 
                    cancellationToken);

                // 6. Si Firebase fue exitoso, confirmamos todo en MySQL
                await _unitOfWork.CommitAsync(cancellationToken);

                // 7. Retornamos el ID para que Angular pueda redirigir a la vista de la escuela
                return school.Id;
            }
            catch (Exception)
            {
                // 8. ROLLBACK: Si Firebase falla, deshacemos MySQL
                await _unitOfWork.RollbackAsync(cancellationToken);
                
                // Lanzamos el error hacia arriba para que la API devuelva un HTTP 500
                throw; 
            }
        }
    }
}