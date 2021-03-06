using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksManager.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nombre de la Editorial")]
        [Required(ErrorMessage = "El campo Nombre no puede ir vacío")]
        public string Name { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "El campo País no puede ir vacío")]
        public string Country { get; set; }

        public List<Book> Books { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

    }
}
