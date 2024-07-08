using System.ComponentModel.DataAnnotations;

namespace Upskilling.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        public string Description { get; set; }

        // Navigational Property(Many)
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
