using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Persistence.Interfaces
{
    // Interfaz de repositorio para la gestion de prestamos
    public interface IPrestamoRepository : IBaseRepository<Prestamo>
    {
    }
}
