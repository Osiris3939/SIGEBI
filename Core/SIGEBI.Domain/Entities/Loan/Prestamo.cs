using System;
using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Loan
{
    // Representa el prestamo de un ejemplar a un usuario
    public class Prestamo : AuditEntity
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
