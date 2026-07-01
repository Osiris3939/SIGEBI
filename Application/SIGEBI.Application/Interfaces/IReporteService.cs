using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Interfaces
{
    // Interfaz de servicio para la gestion de reportes
    public interface IReporteService
    {
        Task<IEnumerable<ReporteDto>> GetAllAsync();
        Task<ReporteDto> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(ReporteDto dto);
        Task<OperationResult> UpdateAsync(ReporteDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
