﻿using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces.TripInterfaces;
using TripFlip.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        private readonly IMapper _mapper;

        public TripsController(ITripService tripService, IMapper mapper)
        {
            _tripService = tripService;
            _mapper = mapper;
        }

        // GET: api/<TripsController>
        [HttpGet]
        public IActionResult Get()
        {
            var trips = _tripService.GetAllTrips();
            var tripViewModels = _mapper.Map<List<TripViewModel>>(trips);

            return Ok(tripViewModels);
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
