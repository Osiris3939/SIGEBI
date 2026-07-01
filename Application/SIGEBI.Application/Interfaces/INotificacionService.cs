using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Interfaces
{
    // Interfaz de servicio para la gestion de notificaciones
    public interface INotificacionService
    {
        Task<IEnumerable<NotificacionDto>> GetAllAsync();
        Task<NotificacionDto> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(NotificacionDto dto);
        Task<OperationResult> UpdateAsync(NotificacionDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
