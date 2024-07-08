using System.ComponentModel.DataAnnotations;

namespace Upskilling.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public string Name { get; set; }

        public string? Descrition { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price is non-negative!")]
        public double Price { get; set; }

        public string Auther { get; set; }
        public int CategoryId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock is non-negative!")]
        public int Stock { get; set; }
        public virtual Category Category { get; set; }
    }
}
