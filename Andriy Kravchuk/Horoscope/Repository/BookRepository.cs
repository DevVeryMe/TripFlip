using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Horoscope.models;

namespace Repository
{
    public class BookRepository : IRepository<Book>
    {
        private string _sqlConnectionString;

        public BookRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public IEnumerable<Book> GetItems()
        {
            List<Book> books = new List<Book>();

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                SqlCommand command = new SqlCommand("spGetBooks", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "spGetBooks"
                };

                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

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
                            Rating = Convert.ToDouble(reader["Rating"]),
                            Publisher = reader["Publisher"].ToString()
                        };

                        books.Add(book);
                    }
                }
            }

            return books;
        }

        public Book GetItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Create(Book item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Book item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
