using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Library
{
    // Representa la categoria de un recurso bibliografico
    public class Categoria : AuditEntity
    {
        public int Id { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
    }
}
