using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Domain.Base;

namespace SIGEBI.Web.Services
{
    // Interfaz para el servicio cliente de consumo de la Web API RESTful
    public interface IApiClientService
    {
        Task<IEnumerable<T>> GetAsync<T>(string endpoint);
        Task<T> GetByIdAsync<T>(string endpoint, int id);
        Task<OperationResult> PostAsync<T>(string endpoint, T data);
        Task<OperationResult> PutAsync<T>(string endpoint, int id, T data);
        Task<OperationResult> DeleteAsync(string endpoint, int id);
    }
}
