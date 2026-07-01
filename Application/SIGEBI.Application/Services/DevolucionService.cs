using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Services
{
    // Servicio de devoluciones
    public class DevolucionService : IDevolucionService
    {
        private readonly IDevolucionRepository _devolucionRepository;

        public DevolucionService(IDevolucionRepository devolucionRepository)
        {
            _devolucionRepository = devolucionRepository;
        }

        public async Task<IEnumerable<DevolucionDto>> GetAllAsync()
        {
            var devoluciones = await _devolucionRepository.GetAllAsync();
            return devoluciones.Select(MapToDto).ToList();
        }

        public async Task<DevolucionDto> GetByIdAsync(int id)
        {
            var devolucion = await _devolucionRepository.GetByIdAsync(id);
            return MapToDto(devolucion);
        }

        public async Task<OperationResult> AddAsync(DevolucionDto dto)
        {
            try
            {
                var devolucion = MapToEntity(dto);
                devolucion.FechaRegistro = DateTime.Now;
                devolucion.UsuarioRegistro = "Sistema";
                devolucion.Estado = true;

                await _devolucionRepository.AddAsync(devolucion);
                return new OperationResult { Success = true, Message = "Devolucion registrada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al registrar devolucion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(DevolucionDto dto)
        {
            try
            {
                var devolucionExistente = await _devolucionRepository.GetByIdAsync(dto.Id);
                if (devolucionExistente == null)
                {
                    return new OperationResult { Success = false, Message = "Devolucion no encontrada." };
                }

                devolucionExistente.PrestamoId = dto.PrestamoId;
                devolucionExistente.FechaEntrega = dto.FechaEntrega;
                devolucionExistente.Observaciones = dto.Observaciones;

                await _devolucionRepository.UpdateAsync(devolucionExistente);
                return new OperationResult { Success = true, Message = "Devolucion actualizada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al actualizar devolucion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                await _devolucionRepository.DeleteAsync(id);
                return new OperationResult { Success = true, Message = "Devolucion eliminada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al eliminar devolucion.", Error = ex.Message };
            }
        }

        private DevolucionDto MapToDto(Devolucion entity)
        {
            if (entity == null) return null;
            return new DevolucionDto
            {
                Id = entity.Id,
                PrestamoId = entity.PrestamoId,
                FechaEntrega = entity.FechaEntrega,
                Observaciones = entity.Observaciones
            };
        }

        private Devolucion MapToEntity(DevolucionDto dto)
        {
            if (dto == null) return null;
            return new Devolucion
            {
                Id = dto.Id,
                PrestamoId = dto.PrestamoId,
                FechaEntrega = dto.FechaEntrega,
                Observaciones = dto.Observaciones
            };
        }
    }
}
