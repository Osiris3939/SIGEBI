using System;
using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Configuration
{
    // Representa un usuario del sistema
    public class Usuario : AuditEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public int RolUsuarioId { get; set; }
        public int TipoUsuarioId { get; set; }
    }
}
