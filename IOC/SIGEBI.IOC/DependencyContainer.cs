using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories;

namespace SIGEBI.IOC
{
    // Registro de dependencias de la aplicacion
    public static class DependencyContainer
    {
        public static IServiceCollection AddSIGEBIServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Registro del contexto de base de datos
            services.AddDbContext<SIGEBIContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Registro de los repositorios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRecursoBibliograficoRepository, RecursoBibliograficoRepository>();
            services.AddScoped<IPrestamoRepository, PrestamoRepository>();
            services.AddScoped<IDevolucionRepository, DevolucionRepository>();
            services.AddScoped<IPenalizacionRepository, PenalizacionRepository>();
            services.AddScoped<INotificacionRepository, NotificacionRepository>();
            services.AddScoped<IReporteRepository, ReporteRepository>();

            // Registro de los servicios de aplicacion
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRecursoBibliograficoService, RecursoBibliograficoService>();
            services.AddScoped<IPrestamoService, PrestamoService>();
            services.AddScoped<IDevolucionService, DevolucionService>();
            services.AddScoped<IPenalizacionService, PenalizacionService>();
            services.AddScoped<INotificacionService, NotificacionService>();
            services.AddScoped<IReporteService, ReporteService>();

            return services;
        }
    }
}
