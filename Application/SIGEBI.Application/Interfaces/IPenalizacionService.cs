using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Interfaces
{
    // Interfaz de servicio para la gestion de penalizaciones
    public interface IPenalizacionService
    {
        Task<IEnumerable<PenalizacionDto>> GetAllAsync();
        Task<PenalizacionDto> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(PenalizacionDto dto);
        Task<OperationResult> UpdateAsync(PenalizacionDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
