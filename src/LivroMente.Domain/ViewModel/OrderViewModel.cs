using LivroMente.Domain.Models.OrderDetailsModel;

namespace LivroMente.Domain.ViewModel
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid AdressId { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime Date { get; set; }
        public float ValueTotal { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}