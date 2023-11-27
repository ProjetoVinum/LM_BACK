using LivroMente.Domain.Models.CategoryBookModel;

namespace LivroMente.Domain.Models.BookModel
{
    public class Book
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
        public String UrlBook { get; set; }
        public String UrlImg { get; set; }
        public CategoryBook Category { get; set; } 
    }
}