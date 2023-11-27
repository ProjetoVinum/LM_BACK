namespace LivroMente.Domain.ViewModel
{
    public class OrderDetailsViewModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public int Amount { get; set; }
        public float ValueUni { get; set; }
    }
}