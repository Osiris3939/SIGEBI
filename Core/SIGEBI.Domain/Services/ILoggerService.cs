using System;

namespace SIGEBI.Domain.Services
{
    // Interfaz para el servicio de registro de logs (Infraestructura)
    public interface ILoggerService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex = null);
    }
}
