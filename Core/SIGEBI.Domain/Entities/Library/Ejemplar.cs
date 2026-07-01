using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Library
{
    // Representa un ejemplar fisico individual de un libro
    public class Ejemplar : AuditEntity
    {
        public int Id { get; set; }
        public string CodigoBarra { get; set; }
        public string EstadoFisico { get; set; }
        public int LibroId { get; set; }

        public virtual Libro Libro { get; set; }
    }
}
