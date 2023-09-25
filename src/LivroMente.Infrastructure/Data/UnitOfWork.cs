using LivroMente.Domain.Core.Data;

namespace LivroMente.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
       private readonly ApplicationDataContext _context;
       public UnitOfWork (ApplicationDataContext context)
       {
        _context = context;
       }
       public void Dispose() => _context.Dispose();
       public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       {
            return await _context.SaveChangesAsync(cancellationToken);
       }
    }
}