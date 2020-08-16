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
        /// Creates a new Route with the given <see cref="CreateRouteViewModel"/> object
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="ResultRouteViewModel"/> object that represents the new entry that was added to database</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateRouteViewModel createRouteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeDto = _mapper.Map<RouteDto>(createRouteViewModel);
            
            var resultDto = await _routeService.CreateAsync(routeDto);

            var resultViewModel = _mapper.Map<ResultRouteViewModel>(resultDto);

            return Ok(resultViewModel);
        }
    }
}
