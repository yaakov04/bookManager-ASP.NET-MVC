using BooksManager.Data;
using BooksManager.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.StoredProcedure
{
    public class spBook
    {
        private readonly BookManagerDBContext _context;
        public spBook(BookManagerDBContext context)
        {
            _context = context;
        }

        public List<BookQuery> getAll()
        {
            List<BookQuery> books = new List<BookQuery>();

            SqlConnection conn = (SqlConnection) _context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "spGetAllBooks";
            cmd.ExecuteNonQuery();
            SqlDataReader rdr = cmd.ExecuteReader();
            BookQuery tmpBook = new BookQuery();
            while (rdr.Read())
            {
                tmpBook.id = (int) rdr["id"];
                tmpBook.title = rdr["title"].ToString();
                tmpBook.year = (int) rdr["year"];
                tmpBook.author = rdr["author"].ToString();
                tmpBook.publisher = rdr["publisher"].ToString();
                tmpBook.category = rdr["category"].ToString();
                books.Add(tmpBook);
            }

            return books;
        }
    }
}
