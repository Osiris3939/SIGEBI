using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services;

namespace SIGEBI.Application.Services
{
    // Servicio de usuarios con Manejo de Errores y Logging integrados
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILoggerService _logger;

        public UsuarioService(IUsuarioRepository usuarioRepository, ILoggerService logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Consultando todos los usuarios de la base de datos.");
                var usuarios = await _usuarioRepository.GetAllAsync();
                return usuarios.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al consultar el listado de usuarios.", ex);
                return new List<UsuarioDto>();
            }
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando usuario por ID: {id}");
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                {
                    _logger.LogWarning($"Usuario con ID: {id} no fue encontrado.");
                }
                return MapToDto(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuario por ID: {id}", ex);
                return null;
            }
        }

        public async Task<OperationResult> AddAsync(UsuarioDto dto)
        {
            try
            {
                _logger.LogInformation($"Registrando nuevo usuario: {dto?.Nombre} {dto?.Apellido}");
                var usuario = MapToEntity(dto);
                usuario.FechaRegistro = DateTime.Now;
                usuario.UsuarioRegistro = "Sistema";
                usuario.Estado = true;

                await _usuarioRepository.AddAsync(usuario);
                _logger.LogInformation($"Usuario registrado exitosamente con ID: {usuario.Id}");
                return new OperationResult { Success = true, Message = "Usuario registrado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al registrar usuario en el servicio.", ex);
                return new OperationResult { Success = false, Message = "Error al registrar usuario.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> UpdateAsync(UsuarioDto dto)
        {
            try
            {
                _logger.LogInformation($"Actualizando información de usuario ID: {dto.Id}");
                var usuarioExistente = await _usuarioRepository.GetByIdAsync(dto.Id);
                if (usuarioExistente == null)
                {
                    _logger.LogWarning($"No se puede actualizar. Usuario ID: {dto.Id} no existe.");
                    return new OperationResult { Success = false, Message = "Usuario no encontrado." };
                }

                usuarioExistente.Nombre = dto.Nombre;
                usuarioExistente.Apellido = dto.Apellido;
                usuarioExistente.Correo = dto.Correo;
                if (!string.IsNullOrEmpty(dto.Password))
                {
                    usuarioExistente.Password = dto.Password;
                }
                usuarioExistente.RolUsuarioId = dto.RolUsuarioId;
                usuarioExistente.TipoUsuarioId = dto.TipoUsuarioId;

                await _usuarioRepository.UpdateAsync(usuarioExistente);
                _logger.LogInformation($"Usuario ID: {dto.Id} actualizado exitosamente.");
                return new OperationResult { Success = true, Message = "Usuario actualizado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar usuario ID: {dto.Id}", ex);
                return new OperationResult { Success = false, Message = "Error al actualizar usuario.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando usuario ID: {id}");
                await _usuarioRepository.DeleteAsync(id);
                _logger.LogInformation($"Usuario ID: {id} eliminado exitosamente.");
                return new OperationResult { Success = true, Message = "Usuario eliminado exitosamente." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar usuario ID: {id}", ex);
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
