namespace Upskilling.DTOs
{
    public class CategoryWithBooks
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual List<string> Books { get; set; } = new List<string>();
    }
}
