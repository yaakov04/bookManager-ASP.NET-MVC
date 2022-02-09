using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksManager.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nombre de la categoría")]
        [Required(ErrorMessage = "El campo Nombre no puede ir vacío")]
        public string Name { get; set; }

        public List<Book> Books { get; set; }


        public DateTime Created { get; set; } = DateTime.Now;

    }
}
