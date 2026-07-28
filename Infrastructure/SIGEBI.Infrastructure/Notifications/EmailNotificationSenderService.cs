using System.Threading.Tasks;
using SIGEBI.Domain.Services;

namespace SIGEBI.Infrastructure.Notifications
{
    // Implementacion concreta para envio de notificaciones via Correo Electronico
    public class EmailNotificationSenderService : INotificationSenderService
    {
        private readonly ILoggerService _logger;

        public EmailNotificationSenderService(ILoggerService logger)
        {
            _logger = logger;
        }

        public Task SendNotificationAsync(string recipient, string subject, string message)
        {
            _logger.LogInformation($"[EMAIL SENDER] Enviando correo a {recipient} | Asunto: {subject}");
            return Task.CompletedTask;
        }
    }
}
