using LivroMente.Domain.Models.CategoryBookModel;

namespace LivroMente.Domain.ViewModel
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string  Title { get; set; }
        public string Author { get; set; }
        public string Synopsis { get; set; } //sinopse
        public int Quantity { get; set; }
        public int Pages { get; set; }
        public string PublishingCompany { get; set; }
        public string Isbn { get; set; }
        public double  Value { get; set; }
        public string Language { get; set; }
        public int Classification { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public virtual CategoryBook Category { get; set; } 
    }
}