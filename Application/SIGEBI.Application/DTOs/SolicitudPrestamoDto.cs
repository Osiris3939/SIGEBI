using System;

namespace SIGEBI.Application.DTOs
{
    // DTO de solicitud de prestamo
    public class SolicitudPrestamoDto
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
    }
}
