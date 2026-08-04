using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;

namespace SIGEBI.Web.Services
{
    // Servicio de consumo de API de penalizaciones para la Capa de Presentación Web
    public class PenalizacionApiConsumerService : IPenalizacionService
    {
        private readonly IApiClientService _apiClient;
        private const string Endpoint = "api/Penalizacion";

        public PenalizacionApiConsumerService(IApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<PenalizacionDto>> GetAllAsync()
        {
            return await _apiClient.GetAsync<PenalizacionDto>(Endpoint);
        }

        public async Task<PenalizacionDto> GetByIdAsync(int id)
        {
            return await _apiClient.GetByIdAsync<PenalizacionDto>(Endpoint, id);
        }

        public async Task<OperationResult> AddAsync(PenalizacionDto dto)
        {
            return await _apiClient.PostAsync(Endpoint, dto);
        }

        public async Task<OperationResult> UpdateAsync(PenalizacionDto dto)
        {
            return await _apiClient.PutAsync(Endpoint, dto.Id, dto);
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            return await _apiClient.DeleteAsync(Endpoint, id);
        }
    }
}
