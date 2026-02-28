using Liggo.Application;
using Liggo.Application.Interfaces;
using Liggo.Infrastructure;
using Liggo.Infrastructure.Services;
using Liggo.Api.Middlewares;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Path.Combine(builder.Environment.ContentRootPath, "FirebaseKey.json"));

// 1. REGISTRAR LAS CAPAS DE CLEAN ARCHITECTURE
// Llama a las configuraciones que hicimos en los días anteriores
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 2. CONFIGURAR LA API WEB
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// REGISTRO DE FIRESTORE (Clean Architecture)
builder.Services.AddSingleton<FirestoreService>();
builder.Services.AddSingleton<IFirestoreTenantRepository>(sp => sp.GetRequiredService<FirestoreService>());

// IMPORTANTE: Configurar CORS por si conectas un Frontend en React/Angular/Vue
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 3. CONFIGURAR EL PIPELINE HTTP (MIDDLEWARES)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// Activa el escudo protector de errores que acabamos de crear
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

// ¡Enciende el servidor!
app.Run();