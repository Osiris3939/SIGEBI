using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Persistence.Repositories
{
    // Implementacion concreta del repositorio de penalizaciones
    public class PenalizacionRepository : BaseRepository<Penalizacion>, IPenalizacionRepository
    {
        public PenalizacionRepository(SIGEBIContext context) : base(context)
        {
        }
    }
}
