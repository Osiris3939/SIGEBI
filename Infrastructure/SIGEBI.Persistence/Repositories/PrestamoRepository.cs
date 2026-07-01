using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Persistence.Repositories
{
    // Implementacion concreta del repositorio de prestamos
    public class PrestamoRepository : BaseRepository<Prestamo>, IPrestamoRepository
    {
        public PrestamoRepository(SIGEBIContext context) : base(context)
        {
        }
    }
}
