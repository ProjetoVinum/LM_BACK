using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Infrastructure.Data;


namespace LivroMente.Infrastructure.Repository
{
    public class CategoryBookRepository : RepositoryBase<CategoryBook, Guid>, ICategoryBookRepository
    {
          public CategoryBookRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
          {
            
          }
    }
}