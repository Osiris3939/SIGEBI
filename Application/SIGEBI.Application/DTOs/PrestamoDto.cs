using System;

namespace SIGEBI.Application.DTOs
{
    // DTO de prestamo
    public class PrestamoDto
    {
        public int Id { get; set; }
        public int EjemplarId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionPactada { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public string EstadoPrestamo { get; set; }
    }
}
