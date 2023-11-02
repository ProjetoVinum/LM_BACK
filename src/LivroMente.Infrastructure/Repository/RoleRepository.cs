using LivroMente.Domain.Models.RoleModel;
using LivroMente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LivroMente.Infrastructure.Repository
{
    public class RoleRepository<T> : IRoleRepository<T> where T : class
    {
        private readonly ApplicationDataContext _context;

        public RoleRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}