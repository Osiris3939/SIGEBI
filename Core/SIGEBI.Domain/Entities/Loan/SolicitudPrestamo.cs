using System;
using SIGEBI.Domain.Base;

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
    }
}
