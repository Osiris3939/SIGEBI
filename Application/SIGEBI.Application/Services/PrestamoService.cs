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
    // Servicio de prestamos
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public async Task<IEnumerable<PrestamoDto>> GetAllAsync()
        {
            var prestamos = await _prestamoRepository.GetAllAsync();
            return prestamos.Select(MapToDto).ToList();
        }

        public async Task<PrestamoDto> GetByIdAsync(int id)
        {
            var prestamo = await _prestamoRepository.GetByIdAsync(id);
            return MapToDto(prestamo);
        }

        public async Task<OperationResult> AddAsync(PrestamoDto dto)
        {
            try
            {
                var prestamo = MapToEntity(dto);
                prestamo.FechaRegistro = DateTime.Now;
                prestamo.UsuarioRegistro = "Sistema";
                prestamo.Estado = true;

                await _prestamoRepository.AddAsync(prestamo);
                return new OperationResult { Success = true, Message = "Prestamo registrado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al registrar prestamo.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(PrestamoDto dto)
        {
            try
            {
                var prestamoExistente = await _prestamoRepository.GetByIdAsync(dto.Id);
                if (prestamoExistente == null)
                {
                    return new OperationResult { Success = false, Message = "Prestamo no encontrado." };
                }

                prestamoExistente.EjemplarId = dto.EjemplarId;
                prestamoExistente.UsuarioId = dto.UsuarioId;
                prestamoExistente.FechaPrestamo = dto.FechaPrestamo;
                prestamoExistente.FechaDevolucionPactada = dto.FechaDevolucionPactada;
                prestamoExistente.FechaDevolucionReal = dto.FechaDevolucionReal;
                prestamoExistente.EstadoPrestamo = dto.EstadoPrestamo;

                await _prestamoRepository.UpdateAsync(prestamoExistente);
                return new OperationResult { Success = true, Message = "Prestamo actualizado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al actualizar prestamo.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                await _prestamoRepository.DeleteAsync(id);
                return new OperationResult { Success = true, Message = "Prestamo eliminado exitosamente." };
            }
            catch (Exception ex)
            {
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
