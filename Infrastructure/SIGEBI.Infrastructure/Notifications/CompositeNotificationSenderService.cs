using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Domain.Services;

namespace SIGEBI.Infrastructure.Notifications
{
    // Demostracion de POLIMORFISMO: despacha llamadas a multiples servicios de notificaciones
    public class CompositeNotificationSenderService : INotificationSenderService
    {
        private readonly IEnumerable<INotificationSenderService> _senders;

        public CompositeNotificationSenderService(IEnumerable<INotificationSenderService> senders)
        {
            _senders = senders ?? new List<INotificationSenderService>();
        }

        public async Task SendNotificationAsync(string recipient, string subject, string message)
        {
            foreach (var sender in _senders)
            {
                await sender.SendNotificationAsync(recipient, subject, message);
            }
        }
    }
}
