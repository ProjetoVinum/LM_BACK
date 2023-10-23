using LivroMente.Domain.Core.Data;
using LivroMente.Domain.Models.IdentityEntities;

namespace LivroMente.Domain.Models.UserModel
{
    public interface IUserRepository <T> where T : class
    {
         List<LivroMente.Domain.Models.IdentityEntities.UserRole> GetUserRolesInclude();
         Task<T> GetById(string id); 
         Task<bool> SaveChangesAsync(); //Salvar
    }
}