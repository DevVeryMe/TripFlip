using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.Interfaces.TripInterfaces;
using TripFlip.ViewModels.TripViewModels;

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

        /// <summary>
        /// Gets all trips.
        /// </summary>
        [HttpGet]
        [Route("/trips")]
        public async Task<IActionResult> GetAsync()
        {
            var trips = await _tripService.GetAllTripsAsync();
            var tripViewModels = _mapper.Map<List<TripViewModel>>(trips);

            return Ok(tripViewModels);
        }

        /// <summary>
        /// Gets trip by id.
        /// </summary>
        /// <param name="id">trip id</param>
        [HttpGet]
        [Route("/trips/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int id)
        {
            var trip = await _tripService.GetAsync(id);
            var tripViewModel = _mapper.Map<TripViewModel>(trip);

            return Ok(tripViewModel);
        }

        /// <summary>
        /// Creates trip.
        /// </summary>
        /// <param name="createTripViewModel">new trip data</param>
        /// <returns>created trip</returns>
        [HttpPost]
        [Route("/trips/add-trip")]
        public async Task<IActionResult> Create([FromBody] CreateTripViewModel createTripViewModel)
        {
            var createTripDto = _mapper.Map<CreateTripDto>(createTripViewModel);
            var tripDto = await _tripService.CreateAsync(createTripDto);
            var tripViewModel = _mapper.Map<TripViewModel>(tripDto);

            return Ok(tripViewModel);
        }

        /// <summary>
        /// Updates existing trip.
        /// </summary>
        /// <param name="tripViewModel">new trip data with existing trip id</param>
        /// <returns>updated trip</returns>
        [HttpPut]
        [Route("/trips/update-trip")]
        public async Task<IActionResult> Update([FromBody] TripViewModel tripViewModel)
        {
            var tripDto = _mapper.Map<TripDto>(tripViewModel);
            tripDto = await _tripService.UpdateAsync(tripDto);
            tripViewModel = _mapper.Map<TripViewModel>(tripDto);

            return Ok(tripViewModel);
        }

        [HttpDelete]
        [Route("/trips/delete-trip")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tripService.DeleteAsync(id);

            return Ok();
        }
    }
}
