using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;

namespace SIGEBI.Web.Services
{
    // Servicio de consumo de API de recursos bibliográficos para la Capa de Presentación Web
    public class RecursoApiConsumerService : IRecursoBibliograficoService
    {
        private readonly IApiClientService _apiClient;
        private const string Endpoint = "api/RecursoBibliografico";

        public RecursoApiConsumerService(IApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<RecursoBibliograficoDto>> GetAllAsync()
        {
            return await _apiClient.GetAsync<RecursoBibliograficoDto>(Endpoint);
        }

        public async Task<RecursoBibliograficoDto> GetByIdAsync(int id)
        {
            return await _apiClient.GetByIdAsync<RecursoBibliograficoDto>(Endpoint, id);
        }

        public async Task<OperationResult> AddAsync(RecursoBibliograficoDto dto)
        {
            return await _apiClient.PostAsync(Endpoint, dto);
        }

        public async Task<OperationResult> UpdateAsync(RecursoBibliograficoDto dto)
        {
            return await _apiClient.PutAsync(Endpoint, dto.Id, dto);
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            return await _apiClient.DeleteAsync(Endpoint, id);
        }
    }
}
