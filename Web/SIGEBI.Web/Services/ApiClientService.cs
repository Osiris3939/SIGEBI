using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Services;

namespace SIGEBI.Web.Services
{
    // Servicio desacoplado de consumo HTTP REST mediante IHttpClientFactory
    public class ApiClientService : IApiClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerService _logger;

        public ApiClientService(IHttpClientFactory httpClientFactory, ILoggerService logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient("SIGEBI_API");
        }

        public async Task<IEnumerable<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var client = CreateClient();
                _logger.LogInformation($"[HTTP GET] Solicitando recurso a la API: {endpoint}");
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
                    return result ?? new List<T>();
                }
                
                _logger.LogWarning($"[HTTP GET] La API respondio con codigo: {response.StatusCode} en {endpoint}");
                return new List<T>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"[API NO DISPONIBLE] Error de conexion al consumir {endpoint}.", ex);
                return new List<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR HTTP GET] Excepcion inesperada al consumir {endpoint}.", ex);
                return new List<T>();
            }
        }

        public async Task<T> GetByIdAsync<T>(string endpoint, int id)
        {
            try
            {
                var client = CreateClient();
                _logger.LogInformation($"[HTTP GET] Solicitando entidad ID {id} en: {endpoint}/{id}");
                var response = await client.GetAsync($"{endpoint}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }

                _logger.LogWarning($"[HTTP GET] Entidad ID {id} no encontrada o codigo {response.StatusCode}");
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR HTTP GET] Fallo al consultar {endpoint}/{id}", ex);
                return default;
            }
        }

        public async Task<OperationResult> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                var client = CreateClient();
                _logger.LogInformation($"[HTTP POST] Enviando datos de creacion a: {endpoint}");
                var response = await client.PostAsJsonAsync(endpoint, data);

                if (response.IsSuccessStatusCode)
                {
                    return new OperationResult { Success = true, Message = "Registro creado exitosamente via API REST." };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning($"[HTTP POST] Error de la API ({response.StatusCode}): {errorContent}");
                return new OperationResult { Success = false, Message = $"Error en API ({response.StatusCode}).", Error = errorContent };
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR HTTP POST] Excepcion al enviar datos a {endpoint}", ex);
                return new OperationResult { Success = false, Message = "API no disponible o error de conexion.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> PutAsync<T>(string endpoint, int id, T data)
        {
            try
            {
                var client = CreateClient();
                _logger.LogInformation($"[HTTP PUT] Actualizando registro ID {id} en: {endpoint}/{id}");
                var response = await client.PutAsJsonAsync($"{endpoint}/{id}", data);

                if (response.IsSuccessStatusCode)
                {
                    return new OperationResult { Success = true, Message = "Registro actualizado exitosamente via API REST." };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning($"[HTTP PUT] Error de la API ({response.StatusCode}): {errorContent}");
                return new OperationResult { Success = false, Message = $"Error al actualizar ({response.StatusCode}).", Error = errorContent };
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR HTTP PUT] Excepcion al actualizar {endpoint}/{id}", ex);
                return new OperationResult { Success = false, Message = "Fallo de conexion con la API.", Error = ex.Message };
            }
        }

        public async Task<OperationResult> DeleteAsync(string endpoint, int id)
        {
            try
            {
                var client = CreateClient();
                _logger.LogInformation($"[HTTP DELETE] Eliminando registro ID {id} en: {endpoint}/{id}");
                var response = await client.DeleteAsync($"{endpoint}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return new OperationResult { Success = true, Message = "Registro eliminado exitosamente via API REST." };
                }

                return new OperationResult { Success = false, Message = $"No se pudo eliminar el registro (HTTP {response.StatusCode})." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ERROR HTTP DELETE] Fallo al eliminar en {endpoint}/{id}", ex);
                return new OperationResult { Success = false, Message = "Fallo de conexion con la API.", Error = ex.Message };
            }
        }
    }
}
