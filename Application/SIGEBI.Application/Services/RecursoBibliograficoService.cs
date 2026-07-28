using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Library;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services;

namespace SIGEBI.Application.Services
{
    // Servicio de recursos bibliograficos con Manejo de Errores y Logging integrados
    public class RecursoBibliograficoService : IRecursoBibliograficoService
    {
        private readonly IRecursoBibliograficoRepository _recursoRepository;
        private readonly ILoggerService _logger;

        public RecursoBibliograficoService(IRecursoBibliograficoRepository recursoRepository, ILoggerService logger)
        {
            _recursoRepository = recursoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<RecursoBibliograficoDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Consultando catalogo completo de recursos bibliograficos.");
                var recursos = await _recursoRepository.GetAllAsync();
                return recursos.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los recursos bibliograficos.", ex);
                return new List<RecursoBibliograficoDto>();
            }
        }

        public async Task<RecursoBibliograficoDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando recurso bibliografico ID: {id}");
                var recurso = await _recursoRepository.GetByIdAsync(id);
                if (recurso == null)
                {
                    _logger.LogWarning($"Recurso bibliografico ID: {id} no encontrado.");
                }
                return MapToDto(recurso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener recurso ID: {id}", ex);
                return null;
            }
        }

        public async Task<OperationResult> AddAsync(RecursoBibliograficoDto dto)
        {
            try
            {
                _logger.LogInformation($"Agregando recurso bibliografico: {dto?.Titulo}");
                var recurso = MapToEntity(dto);
                recurso.FechaRegistro = DateTime.Now;
                recurso.UsuarioRegistro = "Sistema";
                recurso.Estado = true;

                await _recursoRepository.AddAsync(recurso);
                _logger.LogInformation($"Recurso registrado exitosamente ID: {recurso.Id}");
                return new OperationResult { Success = true, Message = "Recurso registrado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al registrar el recurso bibliografico.", ex);
                return new OperationResult { Success = false, Message = "Error al registrar recurso.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(RecursoBibliograficoDto dto)
        {
            try
            {
                _logger.LogInformation($"Actualizando recurso bibliografico ID: {dto.Id}");
                var recursoExistente = await _recursoRepository.GetByIdAsync(dto.Id);
                if (recursoExistente == null)
                {
                    _logger.LogWarning($"Recurso bibliografico ID: {dto.Id} no existe.");
                    return new OperationResult { Success = false, Message = "Recurso no encontrado." };
                }

                recursoExistente.Titulo = dto.Titulo;
                recursoExistente.Autor = dto.Autor;
                recursoExistente.Editorial = dto.Editorial;
                recursoExistente.AnioPublicacion = dto.AnioPublicacion;
                recursoExistente.CategoriaId = dto.CategoriaId;

                await _recursoRepository.UpdateAsync(recursoExistente);
                _logger.LogInformation($"Recurso ID: {dto.Id} actualizado exitosamente.");
                return new OperationResult { Success = true, Message = "Recurso actualizado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar recurso ID: {dto.Id}", ex);
                return new OperationResult { Success = false, Message = "Error al actualizar recurso.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando recurso bibliografico ID: {id}");
                await _recursoRepository.DeleteAsync(id);
                _logger.LogInformation($"Recurso ID: {id} eliminado exitosamente.");
                return new OperationResult { Success = true, Message = "Recurso eliminado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar recurso ID: {id}", ex);
                return new OperationResult { Success = false, Message = "Error al eliminar recurso.", Error = ex.Message };
            }
        }

        private RecursoBibliograficoDto MapToDto(RecursoBibliografico entity)
        {
            if (entity == null) return null;
            return new RecursoBibliograficoDto
            {
                Id = entity.Id,
                Titulo = entity.Titulo,
                Autor = entity.Autor,
                Editorial = entity.Editorial,
                AnioPublicacion = entity.AnioPublicacion,
                CategoriaId = entity.CategoriaId
            };
        }

        private RecursoBibliografico MapToEntity(RecursoBibliograficoDto dto)
        {
            if (dto == null) return null;
            return new RecursoBibliografico
            {
                Id = dto.Id,
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                Editorial = dto.Editorial,
                AnioPublicacion = dto.AnioPublicacion,
                CategoriaId = dto.CategoriaId
            };
        }
    }
}
