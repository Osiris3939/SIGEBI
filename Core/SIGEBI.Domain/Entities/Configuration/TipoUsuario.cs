using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Configuration
{
    // Representa el tipo de usuario
    public class TipoUsuario : AuditEntity
    {
        public int Id { get; set; }
        public string NombreTipo { get; set; }
        public int LimitePrestamos { get; set; }
    }
}
