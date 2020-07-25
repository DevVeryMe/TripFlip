using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using RepositoryLibrary.Models;

namespace RepositoryLibrary
{
    public class SQLCarRepository : IRepository<Car>
    {
        DataContext _db;

        public SQLCarRepository(string connection)
        {
            _db = new DataContext(connection);
        }

        public Car Create(Car car)
        {
            _db.GetTable<Car>().InsertOnSubmit(car);

            return _db.GetTable<Car>().Where(c => c.CarName == car.CarName 
            && c.CarModel == car.CarModel 
            && c.CarMileage == c.CarMileage).FirstOrDefault();
        }

        public void Delete(int id)
        {
            Car car = _db.GetTable<Car>().Where(c => c.Id == id).FirstOrDefault();

            if (car != null)
            {
                _db.GetTable<Car>().DeleteOnSubmit(car);
            }
        }

        public void Dispose()
        {
        }

        public Car GetObject(int id)
        {
            Car car = _db.GetTable<Car>().Where(c => c.Id == id).FirstOrDefault();
            return car;
        }

        public IEnumerable<Car> GetObjectList()
        {
            var cars = _db.GetTable<Car>();
            return cars;
        }

        public void Save()
        {
            _db.SubmitChanges();
        }

        public Car Update(int id, Car car)
        {
            Car carToEdit = GetObject(id);

            carToEdit.CarName = car.CarName;
            carToEdit.CarModel = car.CarModel;
            carToEdit.CarMileage = car.CarMileage;
            carToEdit.EngineType = car.EngineType;
            carToEdit.Horsepower = car.Horsepower;
            carToEdit.ManufactureDate = car.ManufactureDate;
            carToEdit.Price = car.Price;

            return _db.GetTable<Car>().Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
