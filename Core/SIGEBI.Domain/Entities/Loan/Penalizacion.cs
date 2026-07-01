using System;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Domain.Entities.Loan
{
    // Representa una penalizacion o multa a un usuario
    public class Penalizacion : AuditEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PrestamoId { get; set; }
        public decimal MontoMulta { get; set; }
        public string Motivo { get; set; }
        public bool Pagada { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Prestamo Prestamo { get; set; }
    }
}
