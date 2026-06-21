using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Interfaces;

namespace SIGEBI.Persistence.Repositories
{
    // Implementacion concreta del repositorio de usuarios
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(SIGEBIContext context) : base(context)
        {
        }
    }
}
