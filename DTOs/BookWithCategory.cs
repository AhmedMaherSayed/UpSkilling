using System.ComponentModel.DataAnnotations;

namespace Upskilling.DTOs
{
    public class BookWithCategory
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string? BookDescription { get; set; }
        public double BookPrice { get; set; }

        public string BookAuthor { get; set; }
        public int CategoryId { get; set; }
        public int BooksInStock { get; set; }
        public string CategoryName { get; set; }
    }
}
