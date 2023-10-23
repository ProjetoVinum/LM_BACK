using Microsoft.AspNetCore.Identity;

namespace LivroMente.Domain.Models.IdentityEntities
{
    public class UserRole : IdentityUserRole<string>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}