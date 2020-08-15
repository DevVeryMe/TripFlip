using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// Returns all trips from database mapping them to TripViewModel.
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
