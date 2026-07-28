using System.Threading.Tasks;

namespace SIGEBI.Domain.Services
{
    // Interfaz para el servicio de envio de notificaciones (Infraestructura)
    public interface INotificationSenderService
    {
        Task SendNotificationAsync(string recipient, string subject, string message);
    }
}
