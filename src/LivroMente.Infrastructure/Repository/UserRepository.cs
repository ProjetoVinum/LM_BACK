using LivroMente.Domain.Models.IdentityEntities;
using LivroMente.Domain.Models.UserModel;
using LivroMente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LivroMente.Infrastructure.Repository
{
    public class UserRepository<T> : IUserRepository<T> where T : class
    {
        private readonly ApplicationDataContext _context;

        public UserRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<T> GetById(string id)
        {
        
         return await _context.Set<T>().FindAsync(id);
        
        }


        public List<UserRole> GetUserRolesInclude()
        {
             IQueryable<UserRole> entity =  _context.UserRole
            .Include(p=>p.User)
            .Include(p=>p.Role);
           
            return entity.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}