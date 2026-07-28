using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services;

namespace SIGEBI.Application.Services
{
    // Servicio de penalizaciones con Manejo de Errores y Logging integrados
    public class PenalizacionService : IPenalizacionService
    {
        private readonly IPenalizacionRepository _penalizacionRepository;
        private readonly ILoggerService _logger;

        public PenalizacionService(IPenalizacionRepository penalizacionRepository, ILoggerService logger)
        {
            _penalizacionRepository = penalizacionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PenalizacionDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Consultando penalizaciones registradas.");
                var penalizaciones = await _penalizacionRepository.GetAllAsync();
                return penalizaciones.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener las penalizaciones.", ex);
                return new List<PenalizacionDto>();
            }
        }

        public async Task<PenalizacionDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando penalizacion ID: {id}");
                var penalizacion = await _penalizacionRepository.GetByIdAsync(id);
                if (penalizacion == null)
                {
                    _logger.LogWarning($"Penalizacion ID: {id} no encontrada.");
                }
                return MapToDto(penalizacion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener penalizacion ID: {id}", ex);
                return null;
            }
        }

        public async Task<OperationResult> AddAsync(PenalizacionDto dto)
        {
            try
            {
                _logger.LogInformation($"Registrando multa para usuario ID: {dto?.UsuarioId}, monto: RD$ {dto?.MontoMulta}");
                var penalizacion = MapToEntity(dto);
                penalizacion.FechaRegistro = DateTime.Now;
                penalizacion.UsuarioRegistro = "Sistema";
                penalizacion.Estado = true;

                await _penalizacionRepository.AddAsync(penalizacion);
                _logger.LogInformation($"Penalización registrada exitosamente ID: {penalizacion.Id}");
                return new OperationResult { Success = true, Message = "Penalizacion registrada exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al registrar la penalizacion.", ex);
                return new OperationResult { Success = false, Message = "Error al registrar penalizacion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(PenalizacionDto dto)
        {
            try
            {
                _logger.LogInformation($"Actualizando penalizacion ID: {dto.Id}");
                var penalizacionExistente = await _penalizacionRepository.GetByIdAsync(dto.Id);
                if (penalizacionExistente == null)
                {
                    _logger.LogWarning($"Penalizacion ID: {dto.Id} no existe.");
                    return new OperationResult { Success = false, Message = "Penalizacion no encontrada." };
                }

                penalizacionExistente.UsuarioId = dto.UsuarioId;
                penalizacionExistente.PrestamoId = dto.PrestamoId;
                penalizacionExistente.MontoMulta = dto.MontoMulta;
                penalizacionExistente.Motivo = dto.Motivo;
                penalizacionExistente.Pagada = dto.Pagada;

                await _penalizacionRepository.UpdateAsync(penalizacionExistente);
                _logger.LogInformation($"Penalización ID: {dto.Id} actualizada exitosamente.");
                return new OperationResult { Success = true, Message = "Penalizacion actualizada exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar penalizacion ID: {dto.Id}", ex);
                return new OperationResult { Success = false, Message = "Error al actualizar penalizacion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando penalizacion ID: {id}");
                await _penalizacionRepository.DeleteAsync(id);
                _logger.LogInformation($"Penalizacion ID: {id} eliminada exitosamente.");
                return new OperationResult { Success = true, Message = "Penalizacion eliminada exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar penalizacion ID: {id}", ex);
                return new OperationResult { Success = false, Message = "Error al eliminar penalizacion.", Error = ex.Message };
            }
        }

        private PenalizacionDto MapToDto(Penalizacion entity)
        {
            if (entity == null) return null;
            return new PenalizacionDto
            {
                Id = entity.Id,
                UsuarioId = entity.UsuarioId,
                PrestamoId = entity.PrestamoId,
                MontoMulta = entity.MontoMulta,
                Motivo = entity.Motivo,
                Pagada = entity.Pagada
            };
        }

        private Penalizacion MapToEntity(PenalizacionDto dto)
        {
            if (dto == null) return null;
            return new Penalizacion
            {
                Id = dto.Id,
                UsuarioId = dto.UsuarioId,
                PrestamoId = dto.PrestamoId,
                MontoMulta = dto.MontoMulta,
                Motivo = dto.Motivo,
                Pagada = dto.Pagada
            };
        }
    }
}
