using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Notification;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services;

namespace SIGEBI.Application.Services
{
    // Servicio de notificaciones con Manejo de Errores y Logging integrados
    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;
        private readonly ILoggerService _logger;

        public NotificacionService(INotificacionRepository notificacionRepository, ILoggerService logger)
        {
            _notificacionRepository = notificacionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<NotificacionDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Consultando historial de notificaciones.");
                var notificaciones = await _notificacionRepository.GetAllAsync();
                return notificaciones.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener las notificaciones.", ex);
                return new List<NotificacionDto>();
            }
        }

        public async Task<NotificacionDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando notificacion ID: {id}");
                var notificacion = await _notificacionRepository.GetByIdAsync(id);
                if (notificacion == null)
                {
                    _logger.LogWarning($"Notificacion ID: {id} no fue encontrada.");
                }
                return MapToDto(notificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener notificacion ID: {id}", ex);
                return null;
            }
        }

        public async Task<OperationResult> AddAsync(NotificacionDto dto)
        {
            try
            {
                _logger.LogInformation($"Enviando notificacion a usuario ID: {dto?.UsuarioId}");
                var notificacion = MapToEntity(dto);
                notificacion.FechaRegistro = DateTime.Now;
                notificacion.UsuarioRegistro = "Sistema";
                notificacion.Estado = true;

                await _notificacionRepository.AddAsync(notificacion);
                _logger.LogInformation($"Notificacion registrada exitosamente ID: {notificacion.Id}");
                return new OperationResult { Success = true, Message = "Notificacion registrada exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al registrar la notificacion.", ex);
                return new OperationResult { Success = false, Message = "Error al registrar notificacion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(NotificacionDto dto)
        {
            try
            {
                _logger.LogInformation($"Actualizando notificacion ID: {dto.Id}");
                var notificacionExistente = await _notificacionRepository.GetByIdAsync(dto.Id);
                if (notificacionExistente == null)
                {
                    _logger.LogWarning($"Notificacion ID: {dto.Id} no existe.");
                    return new OperationResult { Success = false, Message = "Notificacion no encontrada." };
                }

                notificacionExistente.UsuarioId = dto.UsuarioId;
                notificacionExistente.Mensaje = dto.Mensaje;
                notificacionExistente.FechaEnvio = dto.FechaEnvio;
                notificacionExistente.Leida = dto.Leida;

                await _notificacionRepository.UpdateAsync(notificacionExistente);
                _logger.LogInformation($"Notificacion ID: {dto.Id} actualizada exitosamente.");
                return new OperationResult { Success = true, Message = "Notificacion actualizada exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar notificacion ID: {dto.Id}", ex);
                return new OperationResult { Success = false, Message = "Error al actualizar notificacion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando notificacion ID: {id}");
                await _notificacionRepository.DeleteAsync(id);
                _logger.LogInformation($"Notificacion ID: {id} eliminada exitosamente.");
                return new OperationResult { Success = true, Message = "Notificacion eliminada exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar notificacion ID: {id}", ex);
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
