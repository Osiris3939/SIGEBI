using System.Threading.Tasks;
using SIGEBI.Domain.Services;

namespace SIGEBI.Infrastructure.Notifications
{
    // Implementacion concreta para envio de notificaciones via SMS
    public class SmsNotificationSenderService : INotificationSenderService
    {
        private readonly ILoggerService _logger;

        public SmsNotificationSenderService(ILoggerService logger)
        {
            _logger = logger;
        }

        public Task SendNotificationAsync(string recipient, string subject, string message)
        {
            _logger.LogInformation($"[SMS SENDER] Enviando SMS a {recipient} | Mensaje: {message}");
            return Task.CompletedTask;
        }
    }
}
