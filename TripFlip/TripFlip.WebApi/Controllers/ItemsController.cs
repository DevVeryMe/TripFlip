using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels;
using TripFlip.ViewModels.ItemViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/item-lists/{itemListId}/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new item in a certain item list.
        /// </summary>
        /// <param name="itemListId">item list id</param>
        /// <param name="createItemViewModel">new item data</param>
        /// <returns>created item</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([Range(1, int.MaxValue, 
                ErrorMessage = ErrorConstants.IdLessThanOneError)] int itemListId, 
            [FromBody] CreateItemViewModel createItemViewModel)
        {
            var createItemDto = _mapper.Map<CreateItemDto>(createItemViewModel);
            createItemDto.ItemListId = itemListId;

            var itemDto = await _itemService.CreateAsync(createItemDto);
            var itemViewModel = _mapper.Map<ItemViewModel>(itemDto);

            return Ok(itemViewModel);
        }
    }
}
