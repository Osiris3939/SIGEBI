using System;

namespace SIGEBI.Domain.Base
{
    // Clase base para las entidades con campos de auditoria
    public abstract class AuditEntity
    {
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
