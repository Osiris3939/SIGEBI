namespace SIGEBI.Application.DTOs
{
    // DTO de ejemplar fisico
    public class EjemplarDto
    {
        public int Id { get; set; }
        public string CodigoBarra { get; set; }
        public string EstadoFisico { get; set; }
        public int LibroId { get; set; }
    }
}
