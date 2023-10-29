using Microsoft.AspNetCore.Identity;

namespace LivroMente.Domain.Models.IdentityEntities
{
    public class Role : IdentityRole
    {
       // public string Id { get; set; } = Guid.NewGuid().ToString();
        public List<UserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
    }
}