namespace LivroMente.Domain.Core.Data
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class
    {
        void Add(TEntity entity); 
        TEntity GetbyId(TKey id);
        void Update(TEntity entity); 
        IQueryable<TEntity> GetAll();
        IUnitOfWork UnitOfWork {get;}   
    }
}