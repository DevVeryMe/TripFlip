using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels;
using TripFlip.ViewModels.ItemViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/items")]
    [ApiController]
    [Authorize]
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
        /// Creates a new Item.
        /// </summary>
        /// <param name="createItemViewModel">Data of Item to create.</param>
        /// <returns>Item view model of created database entry.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /items
        ///     {
        ///         "title": "New item.",
        ///         "comment": "Is a very important item.",
        ///         "quantity": "5 kg",
        ///         "itemListId": 1
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(ItemViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateItemViewModel createItemViewModel)
        {
            var createItemDto = _mapper.Map<CreateItemDto>(createItemViewModel);

            var createdItemDto = await _itemService.CreateAsync(createItemDto);
            var createdItemViewModel = _mapper.Map<ItemViewModel>(createdItemDto);

            return Ok(createdItemViewModel);
        }

        /// <summary>
        /// Gets all Items of certain Item list.
        /// </summary>
        /// <param name="itemListId">Item list id.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <param name="searchString">Search string to filter data.</param>
        /// <returns>Paged list of Item view models that
        /// represents database entries with the given Item list id.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<ItemViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByItemListIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromQuery] int itemListId,
            [FromQuery] string searchString,
            [FromQuery] PaginationViewModel paginationViewModel)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var pagedItemDtos = await _itemService.GetAllByItemListIdAsync(itemListId, searchString, paginationDto);

            var pagedItemViewModels = _mapper.Map<PagedList<ItemViewModel>>(pagedItemDtos);

            return Ok(pagedItemViewModels);
        }

        /// <summary>
        /// Updates existing Item.
        /// </summary>
        /// <param name="updateItemViewModel">New Item data with existing Item id.</param>
        /// <returns>Item view model that
        /// represents the updated database entry.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /items
        ///     {
        ///         "title": "Updated item.",
        ///         "comment": "Is a very important item.",
        ///         "quantity": "5 kg",
        ///         "itemListId": 1,
        ///         "isCompleted": true
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(ItemViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateItemViewModel updateItemViewModel)
        {
            var updateItemDto = _mapper.Map<UpdateItemDto>(updateItemViewModel);
            var updatedItemDto = await _itemService.UpdateAsync(updateItemDto);
            var updatedItemViewModel = _mapper.Map<ItemViewModel>(updatedItemDto);

            return Ok(updatedItemViewModel);
        }

        /// <summary>
        /// Updates existing Item's completeness.
        /// </summary>
        /// <param name="updateItemCompletenessViewModel">Item data with existing
        /// Item id and state of completeness.</param>
        /// <returns>Item view model that
        /// represents the updated database entry.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /items/change-completeness
        ///     {
        ///         "id": 1,
        ///         "isCompleted": true
        ///     }
        /// </remarks>
        [HttpPut]
        [Route("change-completeness")]
        [ProducesResponseType(typeof(ItemViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCompletenessAsync(
            [FromBody] UpdateItemCompletenessViewModel updateItemCompletenessViewModel)
        {
            var updateItemCompletenessDto = _mapper.Map<UpdateItemCompletenessDto>(updateItemCompletenessViewModel);

            var updatedItemDto = await _itemService.UpdateCompletenessAsync(updateItemCompletenessDto);
            var updatedItemViewModel = _mapper.Map<ItemViewModel>(updatedItemDto);

            return Ok(updatedItemViewModel);
        }

        /// <summary>
        /// Gets Item by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>Item view model that
        /// represents Item database entry.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            var itemDto = await _itemService.GetByIdAsync(id);
            var itemViewModel = _mapper.Map<ItemViewModel>(itemDto);

            return Ok(itemViewModel);
        }

        /// <summary>
        /// Deletes Item by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>No content (HTTP code 204).</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            await _itemService.DeleteByIdAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Sets item assignees for a specified item.
        /// </summary>
        /// <param name="itemAssigneesViewModel">Object that contains item id 
        /// and route subscribers' ids.</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /items/set-item-assignees
        ///     {
        ///         "itemId": 1,
        ///         "routeSubscriberIds": [1, 2, 3]
        ///     }
        /// </remarks>
        [HttpPut("set-item-assignees")]
        public async Task<IActionResult> SetItemAssigneesAsync(
            [FromBody] ItemAssigneesViewModel itemAssigneesViewModel)
        {
            var itemAssigneesDto = _mapper.Map<ItemAssigneesDto>(itemAssigneesViewModel);

            await _itemService.SetItemAssigneesAsync(itemAssigneesDto);

            return Ok();
        }
    }
}
