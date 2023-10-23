using Microsoft.AspNetCore.Identity;

namespace LivroMente.Domain.Models.IdentityEntities
{
    public class Role : IdentityRole<string>
    {
        public List<UserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
    }
}