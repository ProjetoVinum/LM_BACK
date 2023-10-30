using Microsoft.AspNetCore.Identity;

namespace LivroMente.Domain.Models.IdentityEntities
{
    public class User : IdentityUser
    {
        // public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CompleteName { get; set; }
       // public string? Email { get; set; }
        // public string? Password { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
    }
}