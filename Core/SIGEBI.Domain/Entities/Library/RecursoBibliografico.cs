using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Library
{
    // Representa un recurso bibliografico general de la biblioteca
    public class RecursoBibliografico : AuditEntity
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public int AnioPublicacion { get; set; }
        public int CategoriaId { get; set; }
    }
}
