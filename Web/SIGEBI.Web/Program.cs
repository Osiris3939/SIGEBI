using SIGEBI.IOC;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de MVC (Controladores y Vistas)
builder.Services.AddControllersWithViews();

// Registrar dependencias del sistema SIGEBI
builder.Services.AddSIGEBIServices(builder.Configuration);

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
