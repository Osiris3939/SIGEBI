using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Configuration
{
    // Representa el rol o perfil de un usuario
    public class RolUsuario : AuditEntity
    {
        public int Id { get; set; }
        public string NombreRol { get; set; }
        public string Descripcion { get; set; }
    }
}
