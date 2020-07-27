using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Configuration;
using Repository;
using Repository.Iterfaces;
using Repository.Models;


namespace Horoscope.Controllers
{
    public class CarsController : ApiController
    {
        IRepository<Car> _db;

        public CarsController()
        {
            string connection = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            _db = new SQLCarRepository(connection);
        }

        // GET: api/<CarsController>
        [HttpGet]
        public IEnumerable<Car> GetAllCars()
        {
            return _db.GetObjectList();
        }

        // GET api/<CarsController>/5
        [HttpGet]
        public Car GetCar(int id)
        {
            Car car = _db.GetObject(id);
            return _db.GetObject(id);
        }

        // POST api/<CarsController>
        [HttpPost]
        public Car CreateCar([FromBody] Car car)
        {
            Car createdCar = _db.Create(car);
            _db.Save();

            return createdCar;
        }

        // PUT api/<CarsController>/5
        [HttpPut]
        public Car EditCar(int id, [FromBody] Car car)
        {
            Car editedCar = _db.Update(id, car);
            _db.Save();

            return editedCar;
        }

        // DELETE api/<CarsController>/5
        [HttpDelete]
        public void DeleteCar(int id)
        {
            _db.Delete(id);
            _db.Save();
        }
    }
}
