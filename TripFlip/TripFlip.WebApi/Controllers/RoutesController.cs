using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.Services.DTO;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        IMapper _mapper;
        IRouteService _routeService;

        public RoutesController(IMapper mapper, IRouteService routeService)
        {
            _mapper = mapper;
            _routeService = routeService;
        }

        /// <summary>
        /// Returns a Route with the given Id
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="ResultRouteViewModel"/> object that represents Route database entry</returns>
        [HttpGet("{routeId}")]
        public async Task<IActionResult> GetAsync(int routeId)
        {
            var resultDto = await _routeService.GetAsync(routeId);

            var resultViewModel = _mapper.Map<ResultRouteViewModel>(resultDto);

            return Ok(resultViewModel);
        }

        /// <summary>
        /// Creates a new Route with the given <see cref="CreateRouteViewModel"/> object
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="ResultRouteViewModel"/> object that represents the new entry that was added to database</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateRouteViewModel createRouteViewModel)
        {
            var routeDto = _mapper.Map<RouteDto>(createRouteViewModel);
            
            var resultDto = await _routeService.CreateAsync(routeDto);

            var resultViewModel = _mapper.Map<ResultRouteViewModel>(resultDto);

            return Ok(resultViewModel);
        }

        /// <summary>
        /// Updates Route with the given <see cref="UpdateRouteViewModel"/> object
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="ResultRouteViewModel"/> object that represents the updated database entry</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateRouteViewModel updateRouteViewModel)
        {
            var routeDto = _mapper.Map<RouteDto>(updateRouteViewModel);

            var resultDto = await _routeService.UpdateAsync(routeDto);

            var resultViewModel = _mapper.Map<ResultRouteViewModel>(resultDto);

            return Ok(resultViewModel);
        }
    }
}
