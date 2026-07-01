namespace SIGEBI.Application.DTOs
{
    // DTO de libro
    public class LibroDto : RecursoBibliograficoDto
    {
        public string ISBN { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
