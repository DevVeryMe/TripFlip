﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.Interfaces.TripInterfaces;
using TripFlip.ViewModels;

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
        [Route("all")]
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
        [Route("/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int id)
        {
            var trip = await _tripService.GetAsync(id);
            var tripViewModel = _mapper.Map<TripViewModel>(trip);

            return Ok(tripViewModel);
        }
    }
}
