using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Interfaces;

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
