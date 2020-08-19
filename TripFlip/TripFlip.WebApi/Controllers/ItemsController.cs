using System.Collections.Generic;
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
    [Route("api/items")]
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
        /// <param name="createItemViewModel">new item data</param>
        /// <returns>created item</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateItemViewModel createItemViewModel)
        {
            var createItemDto = _mapper.Map<CreateItemDto>(createItemViewModel);

            var itemDto = await _itemService.CreateAsync(createItemDto);
            var itemViewModel = _mapper.Map<ItemViewModel>(itemDto);

            return Ok(itemViewModel);
        }

        /// <summary>
        /// Returns all items of certain item list.
        /// </summary>
        /// <param name="itemListId">item list id</param>
        /// <returns>collection of items</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllByItemListIdAsync([FromQuery]
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int itemListId)
        {
            var items = await _itemService.GetAllAsync(itemListId);
            var itemViewModels = _mapper.Map<List<ItemViewModel>>(items);

            return Ok(itemViewModels);
        }

        /// <summary>
        /// Updates existing item.
        /// </summary>
        /// <param name="updateItemViewModel">new item data</param>
        /// <returns>updated item</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateItemViewModel updateItemViewModel)
        {
            var itemDtoToUpdate = _mapper.Map<ItemDto>(updateItemViewModel);
            itemDtoToUpdate = await _itemService.UpdateAsync(itemDtoToUpdate);
            var itemViewModelToReturn = _mapper.Map<ItemViewModel>(itemDtoToUpdate);

            return Ok(itemViewModelToReturn);
        }
    }
}
