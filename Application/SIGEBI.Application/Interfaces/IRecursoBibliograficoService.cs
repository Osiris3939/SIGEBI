using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Interfaces
{
    // Interfaz de servicio para la gestion de recursos bibliograficos
    public interface IRecursoBibliograficoService
    {
        Task<IEnumerable<RecursoBibliograficoDto>> GetAllAsync();
        Task<RecursoBibliograficoDto> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(RecursoBibliograficoDto dto);
        Task<OperationResult> UpdateAsync(RecursoBibliograficoDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
