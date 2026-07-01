using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Services
{
    // Servicio de usuarios
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(MapToDto).ToList();
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return MapToDto(usuario);
        }

        public async Task<OperationResult> AddAsync(UsuarioDto dto)
        {
            try
            {
                var usuario = MapToEntity(dto);
                usuario.FechaRegistro = DateTime.Now;
                usuario.UsuarioRegistro = "Sistema";
                usuario.Estado = true;

                await _usuarioRepository.AddAsync(usuario);
                return new OperationResult { Success = true, Message = "Usuario registrado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al registrar usuario.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(UsuarioDto dto)
        {
            try
            {
                var usuarioExistente = await _usuarioRepository.GetByIdAsync(dto.Id);
                if (usuarioExistente == null)
                {
                    return new OperationResult { Success = false, Message = "Usuario no encontrado." };
                }

                usuarioExistente.Nombre = dto.Nombre;
                usuarioExistente.Apellido = dto.Apellido;
                usuarioExistente.Correo = dto.Correo;
                usuarioExistente.Password = dto.Password;
                usuarioExistente.RolUsuarioId = dto.RolUsuarioId;
                usuarioExistente.TipoUsuarioId = dto.TipoUsuarioId;

                await _usuarioRepository.UpdateAsync(usuarioExistente);
                return new OperationResult { Success = true, Message = "Usuario actualizado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al actualizar usuario.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                await _usuarioRepository.DeleteAsync(id);
                return new OperationResult { Success = true, Message = "Usuario eliminado exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al eliminar usuario.", Error = ex.Message };
            }
        }

        private UsuarioDto MapToDto(Usuario entity)
        {
            if (entity == null) return null;
            return new UsuarioDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Correo = entity.Correo,
                Password = entity.Password,
                RolUsuarioId = entity.RolUsuarioId,
                TipoUsuarioId = entity.TipoUsuarioId
            };
        }

        private Usuario MapToEntity(UsuarioDto dto)
        {
            if (dto == null) return null;
            return new Usuario
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Password = dto.Password,
                RolUsuarioId = dto.RolUsuarioId,
                TipoUsuarioId = dto.TipoUsuarioId
            };
        }
    }
}
