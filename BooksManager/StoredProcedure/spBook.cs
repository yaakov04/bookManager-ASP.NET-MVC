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
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                books.Add(new BookQuery
                {
                    id = (int) rdr["id"],
                    title = rdr["title"].ToString(),   
                    year = (int)rdr["year"],
                    author = rdr["author"].ToString(),
                    authorId = (int)rdr["authorId"],
                    publisher = rdr["publisher"].ToString(),
                    publisherId = (int)rdr["publisherId"],
                    category = rdr["category"].ToString(),
                    categoryId = (int) rdr["categoryId"]
                });
            }

            if(rdr.HasRows)
            {
                return books;
            }
            conn.Close();

            return null;
        }

        public BookQuery getById(int? id)
        {
            if(id == null)
            {
                return null;
            }
            List<BookQuery> books = new List<BookQuery>();

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "spGetBookById";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                books.Add(new BookQuery
                {
                    id = (int)rdr["id"],
                    title = rdr["title"].ToString(),
                    year = (int)rdr["year"],
                    author = rdr["author"].ToString(),
                    authorId = (int)rdr["authorId"],
                    publisher = rdr["publisher"].ToString(),
                    publisherId = (int)rdr["publisherId"],
                    category = rdr["category"].ToString(),
                    categoryId = (int)rdr["categoryId"]
                });
            }
            conn.Close();

            if(books.Count == 1)
            {
                return books[0];
            }

            return null;
        }

        public Book getBook(int? id)
        {
            if (id == null)
            {
                return null;
            }
            List<Book> books = new List<Book>();
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "spGetBook";
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                books.Add(new Book
                {
                    Id = (int)rdr["Id"],
                    Title = rdr["Title"].ToString(),
                    PublishedYear = (int)rdr["PublishedYear"],
                    AuthorId = (int) rdr["AuthorId"],
                    PublisherId = (int)rdr["PublisherId"],
                    CategoryId = (int)rdr["CategoryId"]
                });
            }
            conn.Close();

            if (books.Count == 1)
            {
                return books[0];
            }

            return null;
        }

        public int create(Book book)
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
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr.RecordsAffected;
            conn.Close();
        }

        public int update(Book book)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "spUpdateBook";
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = book.Id;
            cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = book.Title;
            cmd.Parameters.Add("@autor", System.Data.SqlDbType.Int).Value = book.AuthorId;
            cmd.Parameters.Add("@year", System.Data.SqlDbType.Int).Value = book.PublishedYear;
            cmd.Parameters.Add("@publisher", System.Data.SqlDbType.Int).Value = book.PublisherId;
            cmd.Parameters.Add("@category", System.Data.SqlDbType.Int).Value = book.CategoryId;
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr.RecordsAffected;
            conn.Close();
        }

        public int delete (int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteBook";
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr.RecordsAffected;
            conn.Close();
        }
    }
}
