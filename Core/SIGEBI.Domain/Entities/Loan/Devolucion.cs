using System;
using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Loan
{
    // Representa la devolucion de un ejemplar prestado
    public class Devolucion : AuditEntity
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Observaciones { get; set; }

        public virtual Prestamo Prestamo { get; set; }
    }
}
