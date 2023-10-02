using LivroMente.Domain.Models.BookModel;
using LivroMente.Infrastructure.Data;

namespace LivroMente.Infrastructure.Repository
{
    public class BookRepository : RepositoryBase<Book, Guid>, IBookRepository
    {
         public BookRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
        {
        }
    }
}