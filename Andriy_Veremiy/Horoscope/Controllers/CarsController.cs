using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLibrary;
using RepositoryLibrary.Models;


namespace Horoscope.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        IRepository<Car> _db;
        IConfiguration _configuration;

        public CarsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new SQLCarRepository(_configuration.GetConnectionString("Default"));
        }

        // GET: api/<CarsController>
        [Route("")]
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return _db.GetObjectList();
        }

        // GET api/<CarsController>/5
        [Route("{id}")]
        [HttpGet]
        public Car Get(int id)
        {
            return _db.GetObject(id);
        }

        // POST api/<CarsController>
        [Route("")]
        [HttpPost]
        public Car Create([FromBody] Car car)
        {
            Car createdCar = _db.Create(car);
            _db.Save();

            return createdCar;
        }

        // PUT api/<CarsController>/5
        [Route("{id}")]
        [HttpPut]
        public Car Edit(int id, [FromBody] Car car)
        {
            Car editedCar = _db.Update(id, car);
            _db.Save();

            return editedCar;
        }

        // DELETE api/<CarsController>/5
        [Route("{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            _db.Delete(id);
            _db.Save();
        }
    }
}
