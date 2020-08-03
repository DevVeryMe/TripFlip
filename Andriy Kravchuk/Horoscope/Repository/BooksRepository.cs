using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Repository.models;

namespace Repository
{
    public class BooksRepository : IBooksRepository
    {
        private string _sqlConnectionString;

        public BooksRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>();

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("spGetBooks", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                sqlConnection.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var book = new Book()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Author = reader["Author"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Count = Convert.ToInt32(reader["Count"]),
                            ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]),
                            Language = Convert.ToString(reader["Language"]),
                            Publisher = reader["Publisher"].ToString()
                        };

                        books.Add(book);
                    }
                }
            }

            return books;
        }

        public Book GetBookById(int id)
        {
            var book = new Book();

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("spGetBook", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);

                sqlConnection.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    book.Id = Convert.ToInt32(reader["Id"]);
                    book.Title = reader["Title"].ToString();
                    book.Description = reader["Description"].ToString();
                    book.Author = reader["Author"].ToString();
                    book.Price = Convert.ToDecimal(reader["Price"]);
                    book.Count = Convert.ToInt32(reader["Count"]);
                    book.ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    book.Language = Convert.ToString(reader["Language"]);
                    book.Publisher = reader["Publisher"].ToString();
                }
            }

            return book;
        }

        public Book Create(Book item)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("spCreateBook", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@Title", item.Title);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Author", item.Author);
                command.Parameters.AddWithValue("@Price", item.Price);
                command.Parameters.AddWithValue("@Count", item.Count);
                command.Parameters.AddWithValue("@ReleaseDate", item.ReleaseDate);
                command.Parameters.AddWithValue("@Language", item.Language);
                command.Parameters.AddWithValue("@Publisher", item.Publisher);

                sqlConnection.Open();
                var id = command.ExecuteScalar();
                item.Id = Convert.ToInt32(id);
            }

            return item;
        }

        public Book Update(Book book)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("spUpdateBook", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@Id", book.Id);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.Parameters.AddWithValue("@Count", book.Count);
                command.Parameters.AddWithValue("@ReleaseDate", book.ReleaseDate);
                command.Parameters.AddWithValue("@Language", book.Language);
                command.Parameters.AddWithValue("@Publisher", book.Publisher);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }

            return book;
        }

        public bool Delete(int id)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("spDeleteBook", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@Id", id);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }

            return true;
        }
    }
}
