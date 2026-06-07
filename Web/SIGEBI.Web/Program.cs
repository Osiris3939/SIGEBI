var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "SIGEBI Web - Estructura inicial del proyecto");

app.Run();
