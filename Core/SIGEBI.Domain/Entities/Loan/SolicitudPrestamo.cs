using System;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Entities.Library;

namespace SIGEBI.Domain.Entities.Loan
{
    // Representa una solicitud de prestamo realizada por un usuario
    public class SolicitudPrestamo : AuditEntity
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }

        public virtual Libro Libro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
