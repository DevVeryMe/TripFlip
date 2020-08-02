using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly string connectionString;
        public UserService(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("DbConnectionString").Value;
        }
        public User Create(User user)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            string sqlExpression = $"INSERT INTO Users VALUES ({user.Id}, '{user.Name}', {user.Age}, " +
                $"'{user.BirthDate.ToString(format)}', '{user.IsWorking}', '{user.Email}', '{user.Country}', " +
                $"'{user.City}', '{user.Phone}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }

            return user;
        }

        public void Delete(long userId)
        {
            string sqlExpression = $"DELETE FROM Users WHERE Id={userId}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            string sqlExpression = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var user = new User
                        {
                            Id = Convert.ToInt64(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Age = Convert.ToInt32(dataReader["Age"]),
                            BirthDate = Convert.ToDateTime(dataReader["BirthDate"]),
                            IsWorking = Convert.ToBoolean(dataReader["IsWorking"]),
                            Email = Convert.ToString(dataReader["Email"]),
                            Country = Convert.ToString(dataReader["Country"]),
                            City = Convert.ToString(dataReader["City"]),
                            Phone = Convert.ToString(dataReader["Phone"])
                        };
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public User GetById(long userId)
        {
            User user = new User();
            string sqlExpression = $"SELECT * FROM Users WHERE Users.Id = {userId}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (!dataReader.HasRows)
                    {
                        throw new NullReferenceException($"There is no user with id {userId}");
                    }
                    while (dataReader.Read())
                    {
                        user = new User
                        {
                            Id = Convert.ToInt64(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Age = Convert.ToInt32(dataReader["Age"]),
                            BirthDate = Convert.ToDateTime(dataReader["BirthDate"]),
                            IsWorking = Convert.ToBoolean(dataReader["IsWorking"]),
                            Email = Convert.ToString(dataReader["Email"]),
                            Country = Convert.ToString(dataReader["Country"]),
                            City = Convert.ToString(dataReader["City"]),
                            Phone = Convert.ToString(dataReader["Phone"])
                        };
                    }
                }
            }

            return user;
        }

        public void Update(User user)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            string sqlExpression = $"UPDATE Users SET Name='{user.Name}', Age={user.Age}, " +
                $"BirthDate='{user.BirthDate.ToString(format)}', IsWorking='{user.IsWorking}', Email='{user.Email}', " +
                $"Country='{user.Country}', City='{user.City}', Phone='{user.Phone}' " +
                $"WHERE Id={user.Id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
            }
        }
    }
}
