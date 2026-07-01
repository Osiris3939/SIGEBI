using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Reports;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Services
{
    // Servicio de reportes
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<IEnumerable<ReporteDto>> GetAllAsync()
        {
            var reportes = await _reporteRepository.GetAllAsync();
            return reportes.Select(MapToDto).ToList();
        }

        public async Task<ReporteDto> GetByIdAsync(int id)
        {
            var reporte = await _reporteRepository.GetByIdAsync(id);
            return MapToDto(reporte);
        }

        public async Task<OperationResult> AddAsync(ReporteDto dto)
        {
            try
            {
                var reporte = MapToEntity(dto);
                reporte.FechaRegistro = DateTime.Now;
                reporte.UsuarioRegistro = "Sistema";
                reporte.Estado = true;

                await _reporteRepository.AddAsync(reporte);
                return new OperationResult { Success = true, Message = "Reporte registrado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al registrar reporte.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(ReporteDto dto)
        {
            try
            {
                var reporteExistente = await _reporteRepository.GetByIdAsync(dto.Id);
                if (reporteExistente == null)
                {
                    return new OperationResult { Success = false, Message = "Reporte no encontrado." };
                }

                reporteExistente.NombreReporte = dto.NombreReporte;
                reporteExistente.TipoReporte = dto.TipoReporte;
                reporteExistente.FechaGeneracion = dto.FechaGeneracion;
                reporteExistente.Contenido = dto.Contenido;

                await _reporteRepository.UpdateAsync(reporteExistente);
                return new OperationResult { Success = true, Message = "Reporte actualizado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al actualizar reporte.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                await _reporteRepository.DeleteAsync(id);
                return new OperationResult { Success = true, Message = "Reporte eliminado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al eliminar reporte.", Error = ex.Message };
            }
        }

        private ReporteDto MapToDto(Reporte entity)
        {
            if (entity == null) return null;
            return new ReporteDto
            {
                Id = entity.Id,
                NombreReporte = entity.NombreReporte,
                TipoReporte = entity.TipoReporte,
                FechaGeneracion = entity.FechaGeneracion,
                Contenido = entity.Contenido
            };
        }

        private Reporte MapToEntity(ReporteDto dto)
        {
            if (dto == null) return null;
            return new Reporte
            {
                Id = dto.Id,
                NombreReporte = dto.NombreReporte,
                TipoReporte = dto.TipoReporte,
                FechaGeneracion = dto.FechaGeneracion,
                Contenido = dto.Contenido
            };
        }
    }
}
