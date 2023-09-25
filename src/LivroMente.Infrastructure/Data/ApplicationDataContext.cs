using LivroMente.Domain.Core.Data;
using LivroMente.Domain.Models.BookModel;
using Microsoft.EntityFrameworkCore;

namespace LivroMente.Infrastructure.Data
{
    public class ApplicationDataContext : DbContext, IUnitOfWork
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) {}
        public DbSet<Book> Books { get; set; }
    }
}