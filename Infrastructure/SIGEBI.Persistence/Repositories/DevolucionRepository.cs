using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Interfaces;

namespace SIGEBI.Persistence.Repositories
{
    // Implementacion concreta del repositorio de devoluciones
    public class DevolucionRepository : BaseRepository<Devolucion>, IDevolucionRepository
    {
        public DevolucionRepository(SIGEBIContext context) : base(context)
        {
        }
    }
}
