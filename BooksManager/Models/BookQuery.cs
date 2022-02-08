namespace BooksManager.Models
{
    public class BookQuery
    {
        public int id { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string author { get; set; }
        public int authorId { get; set; }
        public string publisher { get; set; }
        public int publisherId { get; set; }
        public string category { get; set; }
        public int categoryId { get; set; }
    }
}
