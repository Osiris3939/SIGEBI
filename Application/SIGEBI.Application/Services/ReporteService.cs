using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Reports;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services;

namespace SIGEBI.Application.Services
{
    // Servicio de reportes con Manejo de Errores y Logging integrados
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;
        private readonly ILoggerService _logger;

        public ReporteService(IReporteRepository reporteRepository, ILoggerService logger)
        {
            _reporteRepository = reporteRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ReporteDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Consultando reportes del sistema.");
                var reportes = await _reporteRepository.GetAllAsync();
                return reportes.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los reportes.", ex);
                return new List<ReporteDto>();
            }
        }

        public async Task<ReporteDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando reporte ID: {id}");
                var reporte = await _reporteRepository.GetByIdAsync(id);
                if (reporte == null)
                {
                    _logger.LogWarning($"Reporte ID: {id} no fue encontrado.");
                }
                return MapToDto(reporte);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener reporte ID: {id}", ex);
                return null;
            }
        }

        public async Task<OperationResult> AddAsync(ReporteDto dto)
        {
            try
            {
                _logger.LogInformation($"Generando nuevo reporte: {dto?.NombreReporte}");
                var reporte = MapToEntity(dto);
                reporte.FechaRegistro = DateTime.Now;
                reporte.UsuarioRegistro = "Sistema";
                reporte.Estado = true;

                await _reporteRepository.AddAsync(reporte);
                _logger.LogInformation($"Reporte registrado exitosamente ID: {reporte.Id}");
                return new OperationResult { Success = true, Message = "Reporte registrado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al registrar el reporte.", ex);
                return new OperationResult { Success = false, Message = "Error al registrar reporte.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(ReporteDto dto)
        {
            try
            {
                _logger.LogInformation($"Actualizando reporte ID: {dto.Id}");
                var reporteExistente = await _reporteRepository.GetByIdAsync(dto.Id);
                if (reporteExistente == null)
                {
                    _logger.LogWarning($"Reporte ID: {dto.Id} no existe.");
                    return new OperationResult { Success = false, Message = "Reporte no encontrado." };
                }

                reporteExistente.NombreReporte = dto.NombreReporte;
                reporteExistente.TipoReporte = dto.TipoReporte;
                reporteExistente.FechaGeneracion = dto.FechaGeneracion;
                reporteExistente.Contenido = dto.Contenido;

                await _reporteRepository.UpdateAsync(reporteExistente);
                _logger.LogInformation($"Reporte ID: {dto.Id} actualizado exitosamente.");
                return new OperationResult { Success = true, Message = "Reporte actualizado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar reporte ID: {dto.Id}", ex);
                return new OperationResult { Success = false, Message = "Error al actualizar reporte.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando reporte ID: {id}");
                await _reporteRepository.DeleteAsync(id);
                _logger.LogInformation($"Reporte ID: {id} eliminado exitosamente.");
                return new OperationResult { Success = true, Message = "Reporte eliminado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar reporte ID: {id}", ex);
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
