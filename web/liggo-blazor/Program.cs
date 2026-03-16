using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using liggo_blazor;
using liggo_blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración del HttpClient para la API (Usa la URL de tu API local)
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5208/") 
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<RegistrationService>();
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<IncidentService>();
builder.Services.AddScoped<ReportService>();

await builder.Build().RunAsync();
