using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TripFlip.ViewModels.ItemListViewModels;
using TripFlip.Services.Interfaces;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemListsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IITemListService _itemListService;

        public ItemListsController(IMapper mapper, IITemListService itemListService)
        {
            _mapper = mapper;
            _itemListService = itemListService;
        }

        /// <summary>
        /// Returns an ItemList with the given Id.
        /// </summary>
        /// <returns>If operation is successful, returns <see cref="ResultItemListViewModel"/> object that represents ItemList database entry.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var resultRouteDto = await _itemListService.GetByIdAsync(id);

            var resultRouteViewModel = _mapper.Map<ResultItemListViewModel>(resultRouteDto);

            return Ok(resultRouteViewModel);
        }
    }
}
