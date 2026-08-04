using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;

namespace SIGEBI.Web.Services
{
    // Servicio de consumo de API de usuarios para la Capa de Presentación Web
    public class UsuarioApiConsumerService : IUsuarioService
    {
        private readonly IApiClientService _apiClient;
        private const string Endpoint = "api/Usuario";

        public UsuarioApiConsumerService(IApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            return await _apiClient.GetAsync<UsuarioDto>(Endpoint);
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            return await _apiClient.GetByIdAsync<UsuarioDto>(Endpoint, id);
        }

        public async Task<OperationResult> AddAsync(UsuarioDto dto)
        {
            return await _apiClient.PostAsync(Endpoint, dto);
        }

        public async Task<OperationResult> UpdateAsync(UsuarioDto dto)
        {
            return await _apiClient.PutAsync(Endpoint, dto.Id, dto);
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            return await _apiClient.DeleteAsync(Endpoint, id);
        }
    }
}
