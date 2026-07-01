using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Notification;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Services
{
    // Servicio de notificaciones
    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificacionService(INotificacionRepository notificacionRepository)
        {
            _notificacionRepository = notificacionRepository;
        }

        public async Task<IEnumerable<NotificacionDto>> GetAllAsync()
        {
            var notificaciones = await _notificacionRepository.GetAllAsync();
            return notificaciones.Select(MapToDto).ToList();
        }

        public async Task<NotificacionDto> GetByIdAsync(int id)
        {
            var notificacion = await _notificacionRepository.GetByIdAsync(id);
            return MapToDto(notificacion);
        }

        public async Task<OperationResult> AddAsync(NotificacionDto dto)
        {
            try
            {
                var notificacion = MapToEntity(dto);
                notificacion.FechaRegistro = DateTime.Now;
                notificacion.UsuarioRegistro = "Sistema";
                notificacion.Estado = true;

                await _notificacionRepository.AddAsync(notificacion);
                return new OperationResult { Success = true, Message = "Notificacion registrada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al registrar notificacion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(NotificacionDto dto)
        {
            try
            {
                var notificacionExistente = await _notificacionRepository.GetByIdAsync(dto.Id);
                if (notificacionExistente == null)
                {
                    return new OperationResult { Success = false, Message = "Notificacion no encontrada." };
                }

                notificacionExistente.UsuarioId = dto.UsuarioId;
                notificacionExistente.Mensaje = dto.Mensaje;
                notificacionExistente.FechaEnvio = dto.FechaEnvio;
                notificacionExistente.Leida = dto.Leida;

                await _notificacionRepository.UpdateAsync(notificacionExistente);
                return new OperationResult { Success = true, Message = "Notificacion actualizada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al actualizar notificacion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                await _notificacionRepository.DeleteAsync(id);
                return new OperationResult { Success = true, Message = "Notificacion eliminada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al eliminar notificacion.", Error = ex.Message };
            }
        }

        private NotificacionDto MapToDto(Notificacion entity)
        {
            if (entity == null) return null;
            return new NotificacionDto
            {
                Id = entity.Id,
                UsuarioId = entity.UsuarioId,
                Mensaje = entity.Mensaje,
                FechaEnvio = entity.FechaEnvio,
                Leida = entity.Leida
            };
        }

        private Notificacion MapToEntity(NotificacionDto dto)
        {
            if (dto == null) return null;
            return new Notificacion
            {
                Id = dto.Id,
                UsuarioId = dto.UsuarioId,
                Mensaje = dto.Mensaje,
                FechaEnvio = dto.FechaEnvio,
                Leida = dto.Leida
            };
        }
    }
}
