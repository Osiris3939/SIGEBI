namespace SIGEBI.Application.DTOs
{
    // DTO de penalizacion o multa
    public class PenalizacionDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PrestamoId { get; set; }
        public decimal MontoMulta { get; set; }
        public string Motivo { get; set; }
        public bool Pagada { get; set; }
    }
}
