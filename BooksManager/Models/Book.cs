using System.ComponentModel.DataAnnotations;

namespace BooksManager.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public int PublishedYear { get; set; }


        public Author Author { get; set; }
        [Required]
        public int AuthorId { get; set; }


        public Publisher Publisher { get; set; }
        [Required]
        public int PublisherId { get; set; }


        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }


        public DateTime Created { get; set; } = DateTime.Now;

    }
}
