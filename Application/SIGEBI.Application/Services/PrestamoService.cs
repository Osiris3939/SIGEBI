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
    // Servicio de prestamos con Manejo de Errores y Logging integrados
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly ILoggerService _logger;

        public PrestamoService(IPrestamoRepository prestamoRepository, ILoggerService logger)
        {
            _prestamoRepository = prestamoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PrestamoDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Consultando todos los prestamos.");
                var prestamos = await _prestamoRepository.GetAllAsync();
                return prestamos.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los prestamos.", ex);
                return new List<PrestamoDto>();
            }
        }

        public async Task<PrestamoDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando prestamo ID: {id}");
                var prestamo = await _prestamoRepository.GetByIdAsync(id);
                if (prestamo == null)
                {
                    _logger.LogWarning($"Prestamo ID: {id} no fue encontrado.");
                }
                return MapToDto(prestamo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener prestamo ID: {id}", ex);
                return null;
            }
        }

        public async Task<OperationResult> AddAsync(PrestamoDto dto)
        {
            try
            {
                _logger.LogInformation($"Registrando prestamo para usuario ID: {dto?.UsuarioId}, ejemplar ID: {dto?.EjemplarId}");
                var prestamo = MapToEntity(dto);
                prestamo.FechaRegistro = DateTime.Now;
                prestamo.UsuarioRegistro = "Sistema";
                prestamo.Estado = true;

                await _prestamoRepository.AddAsync(prestamo);
                _logger.LogInformation($"Prestamo registrado con exito ID: {prestamo.Id}");
                return new OperationResult { Success = true, Message = "Prestamo registrado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al registrar el prestamo.", ex);
                return new OperationResult { Success = false, Message = "Error al registrar prestamo.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(PrestamoDto dto)
        {
            try
            {
                _logger.LogInformation($"Actualizando prestamo ID: {dto.Id}");
                var prestamoExistente = await _prestamoRepository.GetByIdAsync(dto.Id);
                if (prestamoExistente == null)
                {
                    _logger.LogWarning($"Prestamo ID: {dto.Id} no existe.");
                    return new OperationResult { Success = false, Message = "Prestamo no encontrado." };
                }

                prestamoExistente.EjemplarId = dto.EjemplarId;
                prestamoExistente.UsuarioId = dto.UsuarioId;
                prestamoExistente.FechaPrestamo = dto.FechaPrestamo;
                prestamoExistente.FechaDevolucionPactada = dto.FechaDevolucionPactada;
                prestamoExistente.FechaDevolucionReal = dto.FechaDevolucionReal;
                prestamoExistente.EstadoPrestamo = dto.EstadoPrestamo;

                await _prestamoRepository.UpdateAsync(prestamoExistente);
                _logger.LogInformation($"Prestamo ID: {dto.Id} actualizado exitosamente.");
                return new OperationResult { Success = true, Message = "Prestamo actualizado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar prestamo ID: {dto.Id}", ex);
                return new OperationResult { Success = false, Message = "Error al actualizar prestamo.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando prestamo ID: {id}");
                await _prestamoRepository.DeleteAsync(id);
                _logger.LogInformation($"Prestamo ID: {id} eliminado exitosamente.");
                return new OperationResult { Success = true, Message = "Prestamo eliminado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar prestamo ID: {id}", ex);
                return new OperationResult { Success = false, Message = "Error al eliminar prestamo.", Error = ex.Message };
            }
        }

        private PrestamoDto MapToDto(Prestamo entity)
        {
            if (entity == null) return null;
            return new PrestamoDto
            {
                Id = entity.Id,
                EjemplarId = entity.EjemplarId,
                UsuarioId = entity.UsuarioId,
                FechaPrestamo = entity.FechaPrestamo,
                FechaDevolucionPactada = entity.FechaDevolucionPactada,
                FechaDevolucionReal = entity.FechaDevolucionReal,
                EstadoPrestamo = entity.EstadoPrestamo
            };
        }

        private Prestamo MapToEntity(PrestamoDto dto)
        {
            if (dto == null) return null;
            return new Prestamo
            {
                Id = dto.Id,
                EjemplarId = dto.EjemplarId,
                UsuarioId = dto.UsuarioId,
                FechaPrestamo = dto.FechaPrestamo,
                FechaDevolucionPactada = dto.FechaDevolucionPactada,
                FechaDevolucionReal = dto.FechaDevolucionReal,
                EstadoPrestamo = dto.EstadoPrestamo
            };
        }
    }
}
