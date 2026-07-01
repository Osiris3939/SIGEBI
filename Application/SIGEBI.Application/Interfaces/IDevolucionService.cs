using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Interfaces
{
    // Interfaz de servicio para la gestion de devoluciones
    public interface IDevolucionService
    {
        Task<IEnumerable<DevolucionDto>> GetAllAsync();
        Task<DevolucionDto> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(DevolucionDto dto);
        Task<OperationResult> UpdateAsync(DevolucionDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
