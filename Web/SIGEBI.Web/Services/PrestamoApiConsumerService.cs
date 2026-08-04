using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;

namespace SIGEBI.Web.Services
{
    // Servicio de consumo de API de préstamos para la Capa de Presentación Web
    public class PrestamoApiConsumerService : IPrestamoService
    {
        private readonly IApiClientService _apiClient;
        private const string Endpoint = "api/Prestamo";

        public PrestamoApiConsumerService(IApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<PrestamoDto>> GetAllAsync()
        {
            return await _apiClient.GetAsync<PrestamoDto>(Endpoint);
        }

        public async Task<PrestamoDto> GetByIdAsync(int id)
        {
            return await _apiClient.GetByIdAsync<PrestamoDto>(Endpoint, id);
        }

        public async Task<OperationResult> AddAsync(PrestamoDto dto)
        {
            return await _apiClient.PostAsync(Endpoint, dto);
        }

        public async Task<OperationResult> UpdateAsync(PrestamoDto dto)
        {
            return await _apiClient.PutAsync(Endpoint, dto.Id, dto);
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            return await _apiClient.DeleteAsync(Endpoint, id);
        }
    }
}
