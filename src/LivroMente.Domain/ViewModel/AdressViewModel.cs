using LivroMente.Domain.Models.IdentityEntities;

namespace LivroMente.Domain.ViewModel
{
    public class AdressViewModel
    {
       public Guid Id { get; set; } 
       public string CEP { get; set; }
       public string City { get; set; }
       public string Neighborhood { get; set; }
       public string Street { get; set; }
       public string Number { get; set; }
       public string State { get; set; }
       public string Complement { get; set; }
<<<<<<< HEAD
       public string Id_User { get; set; } 
       public bool IsActive { get; set; }
       public virtual User User {get; set;}  
=======
       public bool IsActive { get; set; }
       public string UserId { get; set; } 
      // public User User {get; set;}  
>>>>>>> 861e53be271ba1c90c6ce510c43221130f2ebbf9
    }
}