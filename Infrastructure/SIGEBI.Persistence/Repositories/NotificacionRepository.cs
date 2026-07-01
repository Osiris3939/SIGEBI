using SIGEBI.Domain.Entities.Notification;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;

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
