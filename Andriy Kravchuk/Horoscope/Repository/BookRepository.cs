﻿using System;
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
            var book = new Book();

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand("spGetBook", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
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
                    book.Rating = Convert.ToDouble(reader["Rating"]);
                    book.Publisher = reader["Publisher"].ToString();
                }
            }

            return book;
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
