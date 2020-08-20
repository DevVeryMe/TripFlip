using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.RouteDtos;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        /// Returns a Route with the given Id.
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="ResultRouteViewModel"/> object that represents Route database entry.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var resultRouteDto = await _routeService.GetByIdAsync(id);

            var resultRouteViewModel = _mapper.Map<ResultRouteViewModel>(resultRouteDto);

            return Ok(resultRouteViewModel);
        }

        /// <summary>
        /// Returns all Routes with the given Trip Id.
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="List{ResultRouteViewModel}"/> object that represents the list of database entries with the given Trip Id.</returns>
        [HttpGet("trip/{id}")]
        public async Task<IActionResult> GetAllByTripIdAsync(int id)
        {
            var routeDtoList = await _routeService.GetAllByTripIdAsync(id);

            var routeViewModelList = _mapper.Map< List<ResultRouteViewModel> >(routeDtoList);

            return Ok(routeViewModelList);
        }

        /// <summary>
        /// Creates a new Route with the given <see cref="CreateRouteViewModel"/> object.
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="ResultRouteViewModel"/> object that represents the new entry that was added to database.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateRouteViewModel createRouteViewModel)
        {
            var createRouteDto = _mapper.Map<CreateRouteDto>(createRouteViewModel);
            
            var resultRouteDto = await _routeService.CreateAsync(createRouteDto);

            var resultRouteViewModel = _mapper.Map<ResultRouteViewModel>(resultRouteDto);

            return Ok(resultRouteViewModel);
        }

        /// <summary>
        /// Updates Route with the given <see cref="UpdateRouteViewModel"/> object.
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="ResultRouteViewModel"/> object that represents the updated database entry.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateRouteViewModel updateRouteViewModel)
        {
            var updateRouteDto = _mapper.Map<UpdateRouteDto>(updateRouteViewModel);

            var resultRouteDto = await _routeService.UpdateAsync(updateRouteDto);

            var resultViewModel = _mapper.Map<ResultRouteViewModel>(resultRouteDto);

            return Ok(resultViewModel);
        }
    }
}
