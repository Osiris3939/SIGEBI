using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Persistence.Base
{
    // Clase base generica para la implementacion de repositorios
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SIGEBIContext _context;

        public BaseRepository(SIGEBIContext context)
        {
            _context = context;
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
