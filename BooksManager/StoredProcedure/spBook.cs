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
            while (rdr.Read())
            {
                books.Add(new BookQuery
                {
                    id = (int) rdr["id"],
                    title = rdr["title"].ToString(),   
                    year = (int)rdr["year"],
                    author = rdr["author"].ToString(),
                    publisher = rdr["publisher"].ToString(),
                    category = rdr["category"].ToString()
                });
            }
            conn.Close();

            return books;
        }

        public BookQuery getById(int id)
        {
            List<BookQuery> books = new List<BookQuery>();

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "spGetBookById";
            cmd.ExecuteNonQuery();
            SqlDataReader rdr = cmd.ExecuteReader();
            BookQuery tmpBook = new BookQuery();
            while (rdr.Read())
            {
                tmpBook.id = (int)rdr["id"];
                tmpBook.title = rdr["title"].ToString();
                tmpBook.year = (int)rdr["year"];
                tmpBook.author = rdr["author"].ToString();
                tmpBook.publisher = rdr["publisher"].ToString();
                tmpBook.category = rdr["category"].ToString();
                books.Add(tmpBook);
            }
            conn.Close();

            if(books.Count == 1)
            {
                return books[0];
            }

            return null;
        }

        public void create(Book book)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "spCreateBook";
            cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = book.Title;
            cmd.Parameters.Add("@autor", System.Data.SqlDbType.Int).Value = book.AuthorId;
            cmd.Parameters.Add("@year", System.Data.SqlDbType.Int).Value = book.PublishedYear;
            cmd.Parameters.Add("@publisher", System.Data.SqlDbType.Int).Value = book.PublisherId;
            cmd.Parameters.Add("@category", System.Data.SqlDbType.Int).Value = book.CategoryId;
            cmd.Parameters.Add("@datetime", System.Data.SqlDbType.DateTime).Value = book.Created;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
