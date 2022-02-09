using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksManager.Models
{
    public class BookQuery
    {
        [Key]
        public int id { get; set; }

        [DisplayName("Titulo del libro")]
        public string title { get; set; }

        [DisplayName("Año de publicación")]
        public int year { get; set; }

        [DisplayName("Autor")]
        public string author { get; set; }
        public int authorId { get; set; }

        [DisplayName("Editorial")]
        public string publisher { get; set; }
        public int publisherId { get; set; }

        [DisplayName("Categoría")]
        public string category { get; set; }
        public int categoryId { get; set; }
    }
}
