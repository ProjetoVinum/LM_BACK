using Microsoft.AspNetCore.Identity;

namespace LivroMente.Domain.Models.IdentityEntities
{
    public class User : IdentityUser<string>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
    }
}