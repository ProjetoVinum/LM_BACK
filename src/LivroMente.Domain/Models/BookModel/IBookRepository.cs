using LivroMente.Domain.Core.Data;

namespace LivroMente.Domain.Models.BookModel
{
    public interface IBookRepository : IRepository<Book,Guid>
    {
         
    }
}