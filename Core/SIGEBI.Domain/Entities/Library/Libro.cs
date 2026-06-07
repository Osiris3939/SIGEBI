namespace SIGEBI.Domain.Entities.Library
{
    // Representa un libro en la biblioteca
    public class Libro : RecursoBibliografico
    {
        public string ISBN { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
