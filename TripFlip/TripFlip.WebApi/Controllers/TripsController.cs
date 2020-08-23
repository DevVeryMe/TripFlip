using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.Interfaces.TripInterfaces;
using TripFlip.ViewModels;
using TripFlip.ViewModels.TripViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/trips")]
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
        [HttpGet("{id}")]
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateTripViewModel createTripViewModel)
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
        public async Task<IActionResult> UpdateAsync([FromBody] TripViewModel tripViewModel)
        {
            var tripDto = _mapper.Map<TripDto>(tripViewModel);
            tripDto = await _tripService.UpdateAsync(tripDto);
            tripViewModel = _mapper.Map<TripViewModel>(tripDto);

            return Ok(tripViewModel);
        }

        /// <summary>
        /// Deletes trip.
        /// </summary>
        /// <param name="id">trip id</param>
        /// <returns>204 status code</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int id)
        {
            await _tripService.DeleteAsync(id);

            return NoContent();
        }
    }
}
