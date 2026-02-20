using App.Application.Features.Auth;
using App.Domain.Interfaces;
using App.Infrastructure.Identity;
using App.Infrastructure.Persistence.Sql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Conectar a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. Configurar MediatR (Para que encuentre tus Casos de Uso en App.Application)
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(LoginQuery).Assembly));

// 3. Inyección de Dependencias (Conectar los contratos)
builder.Services.AddScoped<IIdentityService, FirebaseIdentityService>();
// builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>(); // Lo activaremos después

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el entorno (Swagger es la interfaz para probar tus rutas)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/ping", () => "ESTA ES LA VERSION 2.0");

app.Run();