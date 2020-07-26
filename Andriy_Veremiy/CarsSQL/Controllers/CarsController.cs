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
        public IEnumerable<Car> Get()
        {
            return _db.GetObjectList();
        }

        // GET api/<CarsController>/5
        public Car Get(int id)
        {
            Car car = _db.GetObject(id);
            return _db.GetObject(id);
        }

        // POST api/<CarsController>
        public Car Create([FromBody] Car car)
        {
            Car createdCar = _db.Create(car);
            _db.Save();

            return createdCar;
        }

        // PUT api/<CarsController>/5
        public Car Edit(int id, [FromBody] Car car)
        {
            Car editedCar = _db.Update(id, car);
            _db.Save();

            return editedCar;
        }

        // DELETE api/<CarsController>/5
        public void Delete(int id)
        {
            _db.Delete(id);
            _db.Save();
        }
    }
}
