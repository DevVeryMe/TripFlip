using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels;
using TripFlip.ViewModels.RouteViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IRouteService _routeService;

        public RoutesController(IMapper mapper, IRouteService routeService)
        {
            _mapper = mapper;
            _routeService = routeService;
        }

        /// <summary>
        /// Gets a Route with the given id.
        /// </summary>
        /// <param name="id">Route id.</param>
        /// <returns>Route view model that
        /// represents Route database entry.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RouteViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            var resultRouteDto = await _routeService.GetByIdAsync(id);

            var resultRouteViewModel = _mapper.Map<RouteViewModel>(resultRouteDto);

            return Ok(resultRouteViewModel);
        }

        /// <summary>
        /// Gets all Routes with the given Trip id.
        /// </summary>
        /// <param name="tripId">Trip id.</param>
        /// <param name="searchString">String to filter routes.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <returns>Paged list of Route view models that
        /// represent database entries with the given Trip id.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<RouteViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByTripIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromQuery] int tripId,
            [FromQuery] string searchString,
            [FromQuery] PaginationViewModel paginationViewModel)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var routeDtosList = await _routeService.GetAllByTripIdAsync(tripId, searchString, paginationDto);

            var routeViewModelsList = _mapper.Map<PagedList<RouteViewModel>>(routeDtosList);

            return Ok(routeViewModelsList);
        }

        /// <summary>
        /// Creates a new Route.
        /// </summary>
        /// <param name="createRouteViewModel">Data to create Route.</param>
        /// <returns>Route view model that
        /// represents the new entry that was added to database.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /routes
        ///     {
        ///         "title": "New route.",
        ///         "tripId": 1
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(RouteViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRouteViewModel createRouteViewModel)
        {
            var createRouteDto = _mapper.Map<CreateRouteDto>(createRouteViewModel);
            
            var resultRouteDto = await _routeService.CreateAsync(createRouteDto);

            var resultRouteViewModel = _mapper.Map<RouteViewModel>(resultRouteDto);

            return Ok(resultRouteViewModel);
        }

        /// <summary>
        /// Updates Route.
        /// </summary>
        /// <param name="updateRouteViewModel">New Route data with existing Route id.</param>
        /// <returns>Route view model
        /// that represents the updated database entry.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /routes
        ///     {
        ///         "id": 1,
        ///         "title": "Updated route.",
        ///         "tripId": 1
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(RouteViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRouteViewModel updateRouteViewModel)
        {
            var updateRouteDto = _mapper.Map<UpdateRouteDto>(updateRouteViewModel);

            var resultRouteDto = await _routeService.UpdateAsync(updateRouteDto);

            var resultViewModel = _mapper.Map<RouteViewModel>(resultRouteDto);

            return Ok(resultViewModel);
        }

        /// <summary>
        /// Deletes Route.
        /// </summary>
        /// <param name="id">Route id.</param>
        /// <returns>No content (HTTP code 204).</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            await _routeService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
