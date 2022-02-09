using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksManager.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nombre del autor")]
        [Required(ErrorMessage = "El campo Nombre no puede ir vacío")]
        public string Name { get; set; }

        [DisplayName("Apellido del Autor")]
        [Required(ErrorMessage = "El campo Apellido no puede ir vacio")]
        public string LastName { get; set; }

        public List<Book> Books { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
