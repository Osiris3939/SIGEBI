using SIGEBI.Domain.Entities.Notification;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Interfaces;

namespace SIGEBI.Persistence.Repositories
{
    // Implementacion concreta del repositorio de notificaciones
    public class NotificacionRepository : BaseRepository<Notificacion>, INotificacionRepository
    {
        public NotificacionRepository(SIGEBIContext context) : base(context)
        {
        }
    }
}
