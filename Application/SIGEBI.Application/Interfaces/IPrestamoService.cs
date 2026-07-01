using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Interfaces
{
    // Interfaz de servicio para la gestion de prestamos
    public interface IPrestamoService
    {
        Task<IEnumerable<PrestamoDto>> GetAllAsync();
        Task<PrestamoDto> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(PrestamoDto dto);
        Task<OperationResult> UpdateAsync(PrestamoDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
