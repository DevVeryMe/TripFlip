using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces;
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
        /// Gets all Trips.
        /// </summary>
        /// <param name="searchString">String to filter Trips.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <returns>Paged list of Trip view models that
        /// represent database entries.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<TripViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] string searchString,
            [FromQuery] PaginationViewModel paginationViewModel)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var pagedTripDtos = await _tripService.GetAllTripsAsync(searchString,
                paginationDto);

            var pagedTripViewModels = _mapper.Map<PagedList<TripViewModel>>(pagedTripDtos);

            return Ok(pagedTripViewModels);
        }

        /// <summary>
        /// Gets Trip by id.
        /// </summary>
        /// <param name="id">Trip id.</param>
        /// <returns>Trip view model that
        /// represents database entry.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TripViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            var tripDto = await _tripService.GetByIdAsync(id);
            var tripViewModel = _mapper.Map<TripViewModel>(tripDto);

            return Ok(tripViewModel);
        }

        /// <summary>
        /// Creates Trip.
        /// </summary>
        /// <param name="createTripViewModel">Data to create Trip.</param>
        /// <returns>Trip view model that
        /// represents the new entry that was added to database.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /trips
        ///     {
        ///         "title": "New trip.",
        ///         "description": "Some trip description.",
        ///         "startsAt": "2020-08-24T10:41:42.604+04:00",     /*yyy-MM-ddThh:mm:ss.ms(offset)*/
        ///         "endsAt": "2020-08-24T10:41:42.604+04:00"        /*yyy-MM-ddThh:mm:ss.ms(offset)*/
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(TripViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTripViewModel createTripViewModel)
        {
            var createTripDto = _mapper.Map<CreateTripDto>(createTripViewModel);
            var createdTripDto = await _tripService.CreateAsync(createTripDto);
            var createdTripViewModel = _mapper.Map<TripViewModel>(createdTripDto);

            return Ok(createdTripViewModel);
        }

        /// <summary>
        /// Updates Trip.
        /// </summary>
        /// <param name="tripViewModel">New Trip data with existing Trip id.</param>
        /// <returns>Trip view model that
        /// represents the updated database entry.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /trips
        ///     {
        ///         "id": 1,
        ///         "title": "Updated trip.",
        ///         "description": "Some trip description.",
        ///         "startsAt": "2020-08-24T10:41:42.604+04:00",     /*yyy-MM-ddThh:mm:ss.ms(offset)*/
        ///         "endsAt": "2020-08-24T10:41:42.604+04:00"        /*yyy-MM-ddThh:mm:ss.ms(offset)*/
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(TripViewModel), StatusCodes.Status200OK)]
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
        /// <param name="id">Trip id.</param>
        /// <returns>No content (HTTP code 204).</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            await _tripService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
