using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        /// <returns></returns>
        [HttpGet]
        [Route("/trips")]
        public async Task<IActionResult> GetAsync()
        {
            var trips = await _tripService.GetAllTripsAsync();
            var tripViewModels = _mapper.Map<List<TripViewModel>>(trips);

            return Ok(tripViewModels);
        }

        
    }
}
