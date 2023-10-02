using LivroMente.Domain.Core.Data;
using LivroMente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LivroMente.Infrastructure.Repository
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly ApplicationDataContext _applicationDataContext;
        protected readonly DbSet<TEntity> _entity;
         public virtual IUnitOfWork UnitOfWork => _applicationDataContext;
         public RepositoryBase(ApplicationDataContext applicationDataContext)
        {
            _applicationDataContext = applicationDataContext;
            _entity= _applicationDataContext.Set<TEntity>();
        }
        //public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(TEntity entity)
        {
             _entity.Add(entity);
        }

        public void Dispose()
        {
             _applicationDataContext.Dispose();
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
           return _entity;
        }

        public TEntity GetbyId(TKey id)
        {
            return _entity.Find(id);
        }

        public void Update(TEntity entity)
        {
             _entity.Update(entity);
        }

         public async Task<int> SaveChangesAsync()
        {
            var result = await _applicationDataContext.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        // Task<IQueryable<TEntity>> IRepository<TEntity, TKey>.GetAll()
        // {
        //     throw new NotImplementedException();
        // }
    }
}