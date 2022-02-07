using BooksManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Data
{
    public class BookManagerDBContext: DbContext
    {
        public BookManagerDBContext(DbContextOptions<BookManagerDBContext> options) : base(options) 
        {
            //
        }

        public DbSet<Book> books { get; set; }
        public DbSet<Author> author { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Publisher> publisher { get; set; }
        public DbSet<BookQuery> BookQuery { get; set; }

        /*
         Migraciones
        - agregar una migracion << add-migration NameOfMigration >>
        - ejecutar la migracion << updadte-database >>

         */
    }
}
