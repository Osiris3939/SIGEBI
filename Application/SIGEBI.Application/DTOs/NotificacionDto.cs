using System;

namespace SIGEBI.Application.DTOs
{
    // DTO de notificacion
    public class NotificacionDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Leida { get; set; }
    }
}
