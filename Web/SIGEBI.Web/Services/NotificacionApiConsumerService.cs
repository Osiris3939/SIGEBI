using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;

namespace SIGEBI.Web.Services
{
    // Servicio de consumo de API de notificaciones para la Capa de Presentación Web
    public class NotificacionApiConsumerService : INotificacionService
    {
        private readonly IApiClientService _apiClient;
        private const string Endpoint = "api/Notificacion";

        public NotificacionApiConsumerService(IApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<NotificacionDto>> GetAllAsync()
        {
            return await _apiClient.GetAsync<NotificacionDto>(Endpoint);
        }

        public async Task<NotificacionDto> GetByIdAsync(int id)
        {
            return await _apiClient.GetByIdAsync<NotificacionDto>(Endpoint, id);
        }

        public async Task<OperationResult> AddAsync(NotificacionDto dto)
        {
            return await _apiClient.PostAsync(Endpoint, dto);
        }

        public async Task<OperationResult> UpdateAsync(NotificacionDto dto)
        {
            return await _apiClient.PutAsync(Endpoint, dto.Id, dto);
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            return await _apiClient.DeleteAsync(Endpoint, id);
        }
    }
}
