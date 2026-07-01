using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Entities.Library;
using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Domain.Entities.Notification;
using SIGEBI.Domain.Entities.Reports;

namespace SIGEBI.Persistence.Context
{
    // Contexto de base de datos principal para el sistema SIGEBI
    public class SIGEBIContext : DbContext
    {
        public SIGEBIContext(DbContextOptions<SIGEBIContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RolUsuario> RolesUsuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<RecursoBibliografico> RecursosBibliograficos { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Ejemplar> Ejemplares { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<SolicitudPrestamo> SolicitudesPrestamos { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
        public DbSet<Penalizacion> Penalizaciones { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
    }
}
