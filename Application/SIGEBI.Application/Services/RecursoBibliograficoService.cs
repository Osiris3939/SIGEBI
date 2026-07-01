using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Library;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Services
{
    // Servicio de recursos bibliograficos
    public class RecursoBibliograficoService : IRecursoBibliograficoService
    {
        private readonly IRecursoBibliograficoRepository _recursoRepository;

        public RecursoBibliograficoService(IRecursoBibliograficoRepository recursoRepository)
        {
            _recursoRepository = recursoRepository;
        }

        public async Task<IEnumerable<RecursoBibliograficoDto>> GetAllAsync()
        {
            var recursos = await _recursoRepository.GetAllAsync();
            return recursos.Select(MapToDto).ToList();
        }

        public async Task<RecursoBibliograficoDto> GetByIdAsync(int id)
        {
            var recurso = await _recursoRepository.GetByIdAsync(id);
            return MapToDto(recurso);
        }

        public async Task<OperationResult> AddAsync(RecursoBibliograficoDto dto)
        {
            try
            {
                var recurso = MapToEntity(dto);
                recurso.FechaRegistro = DateTime.Now;
                recurso.UsuarioRegistro = "Sistema";
                recurso.Estado = true;

                await _recursoRepository.AddAsync(recurso);
                return new OperationResult { Success = true, Message = "Recurso registrado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al registrar recurso.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(RecursoBibliograficoDto dto)
        {
            try
            {
                var recursoExistente = await _recursoRepository.GetByIdAsync(dto.Id);
                if (recursoExistente == null)
                {
                    return new OperationResult { Success = false, Message = "Recurso no encontrado." };
                }

                recursoExistente.Titulo = dto.Titulo;
                recursoExistente.Autor = dto.Autor;
                recursoExistente.Editorial = dto.Editorial;
                recursoExistente.AnioPublicacion = dto.AnioPublicacion;
                recursoExistente.CategoriaId = dto.CategoriaId;

                await _recursoRepository.UpdateAsync(recursoExistente);
                return new OperationResult { Success = true, Message = "Recurso actualizado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al actualizar recurso.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                await _recursoRepository.DeleteAsync(id);
                return new OperationResult { Success = true, Message = "Recurso eliminado exitosamente." };
            }
            catch (Exception ex)
            {
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
