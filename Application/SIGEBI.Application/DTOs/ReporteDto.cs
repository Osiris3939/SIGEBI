using System;

namespace SIGEBI.Application.DTOs
{
    // DTO de reporte
    public class ReporteDto
    {
        public int Id { get; set; }
        public string NombreReporte { get; set; }
        public string TipoReporte { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public string Contenido { get; set; }
    }
}
