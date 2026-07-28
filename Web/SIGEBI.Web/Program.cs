using Microsoft.Extensions.DependencyInjection;
using SIGEBI.IOC;
using SIGEBI.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de MVC (Controladores y Vistas)
builder.Services.AddControllersWithViews();

// Registrar dependencias del sistema SIGEBI
builder.Services.AddSIGEBIServices(builder.Configuration);

var app = builder.Build();

// Inicializar y sembrar datos de prueba en la base de datos (Seeder)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SIGEBIContext>();
    SIGEBIDbSeeder.Initialize(context);
}

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
