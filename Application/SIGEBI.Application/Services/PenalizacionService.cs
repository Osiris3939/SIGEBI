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
    // Servicio de penalizaciones
    public class PenalizacionService : IPenalizacionService
    {
        private readonly IPenalizacionRepository _penalizacionRepository;

        public PenalizacionService(IPenalizacionRepository penalizacionRepository)
        {
            _penalizacionRepository = penalizacionRepository;
        }

        public async Task<IEnumerable<PenalizacionDto>> GetAllAsync()
        {
            var penalizaciones = await _penalizacionRepository.GetAllAsync();
            return penalizaciones.Select(MapToDto).ToList();
        }

        public async Task<PenalizacionDto> GetByIdAsync(int id)
        {
            var penalizacion = await _penalizacionRepository.GetByIdAsync(id);
            return MapToDto(penalizacion);
        }

        public async Task<OperationResult> AddAsync(PenalizacionDto dto)
        {
            try
            {
                var penalizacion = MapToEntity(dto);
                penalizacion.FechaRegistro = DateTime.Now;
                penalizacion.UsuarioRegistro = "Sistema";
                penalizacion.Estado = true;

                await _penalizacionRepository.AddAsync(penalizacion);
                return new OperationResult { Success = true, Message = "Penalizacion registrada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al registrar penalizacion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(PenalizacionDto dto)
        {
            try
            {
                var penalizacionExistente = await _penalizacionRepository.GetByIdAsync(dto.Id);
                if (penalizacionExistente == null)
                {
                    return new OperationResult { Success = false, Message = "Penalizacion no encontrada." };
                }

                penalizacionExistente.UsuarioId = dto.UsuarioId;
                penalizacionExistente.PrestamoId = dto.PrestamoId;
                penalizacionExistente.MontoMulta = dto.MontoMulta;
                penalizacionExistente.Motivo = dto.Motivo;
                penalizacionExistente.Pagada = dto.Pagada;

                await _penalizacionRepository.UpdateAsync(penalizacionExistente);
                return new OperationResult { Success = true, Message = "Penalizacion actualizada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al actualizar penalizacion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                await _penalizacionRepository.DeleteAsync(id);
                return new OperationResult { Success = true, Message = "Penalizacion eliminada exitosamente." };
            }
            catch (Exception ex)
            {
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
