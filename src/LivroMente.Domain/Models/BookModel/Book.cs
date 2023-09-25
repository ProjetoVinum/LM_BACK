namespace LivroMente.Domain.Models.BookModel
{
    public class Book
    {
        public Guid Id { get; set; }
        public string  Title { get; set; }
        public string Author { get; set; }
        public double  Value { get; set; }
    }
}