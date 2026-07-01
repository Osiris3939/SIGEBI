using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Interfaces
{
    // Interfaz de servicio para la gestion de usuarios
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(UsuarioDto dto);
        Task<OperationResult> UpdateAsync(UsuarioDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
