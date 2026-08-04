using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Services;
using SIGEBI.Infrastructure.Logging;
using SIGEBI.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de MVC (Controladores y Vistas)
builder.Services.AddControllersWithViews();

// Registrar Logger Polimorfico para la capa de Presentación
builder.Services.AddSingleton<ConsoleLoggerService>();
builder.Services.AddSingleton<ILoggerService>(sp => sp.GetRequiredService<ConsoleLoggerService>());

// Configurar HttpClientFactory para el consumo desacoplado de la Web API (RESTful Backend)
builder.Services.AddHttpClient("SIGEBI_API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5029/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Registrar Servicio Base de Consumo HTTP
builder.Services.AddScoped<IApiClientService, ApiClientService>();

// Registrar Servicios de Consumo de API por Módulo (Desacoplados de la lógica interna)
builder.Services.AddScoped<IUsuarioService, UsuarioApiConsumerService>();
builder.Services.AddScoped<IRecursoBibliograficoService, RecursoApiConsumerService>();
builder.Services.AddScoped<IPrestamoService, PrestamoApiConsumerService>();
builder.Services.AddScoped<IPenalizacionService, PenalizacionApiConsumerService>();
builder.Services.AddScoped<INotificacionService, NotificacionApiConsumerService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
