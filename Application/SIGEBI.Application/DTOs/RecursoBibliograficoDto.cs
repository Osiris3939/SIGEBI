namespace SIGEBI.Application.DTOs
{
    // DTO de recurso bibliografico
    public class RecursoBibliograficoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public int AnioPublicacion { get; set; }
        public int CategoriaId { get; set; }
    }
}
