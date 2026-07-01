using System;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Domain.Entities.Notification
{
    // Representa una notificacion enviada a un usuario
    public class Notificacion : AuditEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Leida { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
