namespace BooksManager.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PublishedYear { get; set; }


        public Author Author { get; set; }
        public int AuthorId { get; set; }


        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }


        public Category Category { get; set; }
        public int CategoryId { get; set; }


        public DateTime Created { get; set; } = DateTime.Now;

    }
}
