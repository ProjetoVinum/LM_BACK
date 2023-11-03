using LivroMente.Domain.Models.IdentityEntities;

namespace LivroMente.Domain.Models.AdressModel
{
    public class Adress
    {
       public Guid Id { get; set; } 
       public string CEP { get; set; }
       public string City { get; set; }
       public string Neighborhood { get; set; }
       public string Street { get; set; }
       public string Number { get; set; }
       public string State { get; set; }
       public string Complement { get; set; }
       public bool IsActive { get; set; }
       public string UserId { get; set; }
       public User User {get; set;}
    }

}