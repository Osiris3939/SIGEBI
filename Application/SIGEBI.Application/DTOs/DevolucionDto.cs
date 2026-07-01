using System;

namespace SIGEBI.Application.DTOs
{
    // DTO de devolucion
    public class DevolucionDto
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Observaciones { get; set; }
    }
}
