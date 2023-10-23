using LivroMente.Domain.Core.Data;
using LivroMente.Domain.Models.BookModel;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.Models.IdentityEntities;
using LivroMente.Domain.Models.PaymentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LivroMente.Infrastructure.Data
{
    public class ApplicationDataContext : IdentityDbContext<User, Role,string,
            IdentityUserClaim<string>,UserRole,IdentityUserLogin<string>,
            IdentityRoleClaim<string>,IdentityUserToken<string>>,IUnitOfWork
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) {}
         public DbSet<Book> Book { get; set; }
         public DbSet<CategoryBook> CategoryBook { get; set; }
         public DbSet<Payment> Payment {get;set;}
         public DbSet<User> User { get; set; }
         public DbSet<Role> Role { get; set; }
         public DbSet<UserRole> UserRole { get; set; }


         


      
    }
}