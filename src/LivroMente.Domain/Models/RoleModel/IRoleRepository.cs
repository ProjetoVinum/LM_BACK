namespace LivroMente.Domain.Models.RoleModel
{
    public interface IRoleRepository<T> where T : class
    {
          Task<IEnumerable<T>> GetAll();
    }
}