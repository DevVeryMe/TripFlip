using Repository.Models.DotNetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Iterfaces;

namespace Repository.DotNetCore
{
    public class SQLCarRepository : IRepository<Car>
    {
        private string _connectionString;

        public SQLCarRepository(string connection)
        {
            _connectionString = connection;
        }

        public Car Create(Car car)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("sp_CreateCar", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@CarName", car.CarName);
                command.Parameters.AddWithValue("@CarModel", car.CarModel);
                command.Parameters.AddWithValue("@CarMileage", car.CarMileage);
                command.Parameters.AddWithValue("@EngineType", car.EngineType);
                command.Parameters.AddWithValue("@ManufactureDate", car.ManufactureDate);
                command.Parameters.AddWithValue("@Horsepower", car.Horsepower);
                command.Parameters.AddWithValue("@Price", car.Price);

                sqlConnection.Open();

                var id = command.ExecuteScalar();
                car.ID = Convert.ToInt32(id);
            }

            return car;
        }

        public void Delete(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("sp_DeleteCar", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@ID", id);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
        }

        public Car GetObject(int id)
        {
            var car = new Car();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("sp_GetCar", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ID", id);

                sqlConnection.Open();

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    car.ID = Convert.ToInt32(reader["ID"]);
                    car.CarName = reader["CarName"].ToString();
                    car.CarModel = reader["CarModel"].ToString();
                    car.EngineType = reader["EngineType"].ToString();
                    car.Price = Convert.ToDecimal(reader["Price"]);
                    car.Horsepower = Convert.ToInt32(reader["Horsepower"]);
                    car.ManufactureDate = Convert.ToDateTime(reader["ManufactureDate"]);
                    car.CarMileage = Convert.ToSingle(reader["CarMileage"]);
                }
            }

            return car;
        }

        public IEnumerable<Car> GetObjectList()
        {
            var cars = new List<Car>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("sp_GetCars", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlConnection.Open();

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        var readCar = new Car()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            CarName = reader["CarName"].ToString(),
                            CarModel = reader["CarModel"].ToString(),
                            EngineType = reader["EngineType"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Horsepower = Convert.ToInt32(reader["Horsepower"]),
                            ManufactureDate = Convert.ToDateTime(reader["ManufactureDate"]),
                            CarMileage = Convert.ToSingle(reader["CarMileage"]),
                        };

                        cars.Add(readCar);
                    }

                }

            }

            return cars;
        }

        public void Save()
        {
        }

        public Car Update(int id, Car car)
        {
            Car carToEdit = GetObject(id);
            if (carToEdit == null)
            {
                throw new Exception();
            }

            car.ID = carToEdit.ID;

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("sp_UpdateCar", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                command.Parameters.AddWithValue("@ID", car.ID);
                command.Parameters.AddWithValue("@CarName", car.CarName);
                command.Parameters.AddWithValue("@CarModel", car.CarModel);
                command.Parameters.AddWithValue("@CarMileage", car.CarMileage);
                command.Parameters.AddWithValue("@EngineType", car.EngineType);
                command.Parameters.AddWithValue("@ManufactureDate", car.ManufactureDate);
                command.Parameters.AddWithValue("@Horsepower", car.Horsepower);
                command.Parameters.AddWithValue("@Price", car.Price);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }

            return car;
        }
    }
}
