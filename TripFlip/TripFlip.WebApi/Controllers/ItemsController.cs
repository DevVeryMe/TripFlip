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
        /// <param name="createItemViewModel">New item view model.</param>
        /// <returns>Created item view model.</returns>
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
        /// <param name="id">Item list id</param>
        /// <returns>Collection of item view models</returns>
        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetAllByItemListIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int id)
        {
            var items = await _itemService.GetAllAsync(id);
            var itemViewModels = _mapper.Map<List<ItemViewModel>>(items);

            return Ok(itemViewModels);
        }

        /// <summary>
        /// Updates existing item.
        /// </summary>
        /// <param name="updateItemViewModel">Item view model to update</param>
        /// <returns>Updated item view model</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateItemViewModel updateItemViewModel)
        {
            var itemDtoToUpdate = _mapper.Map<UpdateItemDto>(updateItemViewModel);
            var itemDtoToReturn = await _itemService.UpdateAsync(itemDtoToUpdate);
            var itemViewModelToReturn = _mapper.Map<ItemViewModel>(itemDtoToReturn);

            return Ok(itemViewModelToReturn);
        }

        /// <summary>
        /// Gets item by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>Item view model.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemByIdAsync([Range(1, int.MaxValue,
            ErrorMessage = ErrorConstants.IdLessThanOneError)] int id)
        {
            var itemDtoToReturn = await _itemService.GetByIdAsync(id);
            var itemViewModelToReturn = _mapper.Map<ItemViewModel>(itemDtoToReturn);

            return Ok(itemViewModelToReturn);
        }

        /// <summary>
        /// Deletes item by id.
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int id)
        {
            await _itemService.DeleteAsync(id);

            return NoContent();
        }
    }
}
