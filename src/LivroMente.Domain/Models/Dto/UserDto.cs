using System.ComponentModel.DataAnnotations;

namespace LivroMente.Domain.Models.Dto
{
    public class UserDto
    {
        public string CompleteName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword{ get; set; }

         public string Role { get; set; }

        public bool IsActive { get; set; }

        
    }
}