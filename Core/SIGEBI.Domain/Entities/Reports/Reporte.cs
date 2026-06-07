using System;
using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.Reports
{
    // Representa un reporte generado por el sistema
    public class Reporte : AuditEntity
    {
        public int Id { get; set; }
        public string NombreReporte { get; set; }
        public string TipoReporte { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public string Contenido { get; set; }
    }
}
