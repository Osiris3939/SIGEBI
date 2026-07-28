using Microsoft.Extensions.DependencyInjection;
using SIGEBI.IOC;
using SIGEBI.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar los servicios de la solucion SIGEBI
builder.Services.AddSIGEBIServices(builder.Configuration);

var app = builder.Build();

// Inicializar y sembrar datos de prueba en la base de datos (Seeder)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SIGEBIContext>();
    SIGEBIDbSeeder.Initialize(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "SIGEBI API - Estructura inicial del proyecto");

app.MapControllers();

app.Run();
