using SIGEBI.Domain.Entities.Library;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Interfaces;

namespace SIGEBI.Persistence.Repositories
{
    // Implementacion concreta del repositorio de recursos bibliograficos
    public class RecursoBibliograficoRepository : BaseRepository<RecursoBibliografico>, IRecursoBibliograficoRepository
    {
        public RecursoBibliograficoRepository(SIGEBIContext context) : base(context)
        {
        }
    }
}
