using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksManager.Models
{
    public class Book
    {
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage ="El campo Título no puede ir vacío")]
        public string Title { get; set; }

        [DisplayName("Año de publicación")]
        [Required(ErrorMessage = "El campo Año de publicacion no puede ir vacío")]
        public int? PublishedYear { get; set; }

        public Author Author { get; set; }
        [DisplayName("Autor")]
        [Required(ErrorMessage = "Debe seleccionar una opcion")]
        public int? AuthorId { get; set; }


        public Publisher Publisher { get; set; }
        [DisplayName("Editorial")]
        [Required(ErrorMessage = "Debe seleccionar una opcion")]
        public int? PublisherId { get; set; }


        public Category Category { get; set; }
        [DisplayName("Categoría")]
        [Required(ErrorMessage = "Debe seleccionar una opcion")]
        public int? CategoryId { get; set; }


        public DateTime Created { get; set; } = DateTime.Now;

    }
}
