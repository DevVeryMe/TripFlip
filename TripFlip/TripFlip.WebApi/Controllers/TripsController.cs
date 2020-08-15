using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces.TripInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        // GET: api/<TripsController>
        [HttpGet]
        public IEnumerable<TripDto> Get()
        {
            var trips = _tripService.GetAllTrips();
            return trips;
        }

        // GET api/<TripsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TripsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TripsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TripsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
