using SIGEBI.Domain.Entities.Reports;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Persistence.Repositories
{
    // Implementacion concreta del repositorio de reportes
    public class ReporteRepository : BaseRepository<Reporte>, IReporteRepository
    {
        public ReporteRepository(SIGEBIContext context) : base(context)
        {
        }
    }
}
