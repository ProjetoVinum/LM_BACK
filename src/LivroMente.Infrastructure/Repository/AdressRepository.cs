using LivroMente.Domain.Models.AdressModel;
using LivroMente.Infrastructure.Data;

namespace LivroMente.Infrastructure.Repository
{
    public class AdressRepository: RepositoryBase<Adress, Guid>, IAdressRepository
    {
         public AdressRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }   
    }
}