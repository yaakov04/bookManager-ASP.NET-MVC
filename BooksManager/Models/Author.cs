using System.ComponentModel.DataAnnotations;

namespace BooksManager.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<Book> Books { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
