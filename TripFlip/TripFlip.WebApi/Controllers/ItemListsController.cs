using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels;
using TripFlip.ViewModels.ItemListViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/item-lists")]
    [ApiController]
    public class ItemListsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IItemListService _itemListService;

        public ItemListsController(IMapper mapper, IItemListService itemListService)
        {
            _mapper = mapper;
            _itemListService = itemListService;
        }

        /// <summary>
        /// Gets Item list by its id.
        /// </summary>
        /// <param name="id">Id of Item list.</param>
        /// <returns>Item list view model that
        /// represents item list database entry.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemListViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            var routeDto = await _itemListService.GetByIdAsync(id);

            var routeViewModel = _mapper.Map<ItemListViewModel>(routeDto);

            return Ok(routeViewModel);
        }

        /// <summary>
        /// Gets all Item lists with the given Route Id.
        /// </summary>
        /// <param name="routeId">Id of Route.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <param name="searchString">String to filter data.</param>
        /// <returns>Paged list with Item list view models that
        /// represent database entries with the given Route id.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<ItemListViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByRouteIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromQuery] int routeId, 
            [FromQuery] PaginationViewModel paginationViewModel,
            [FromQuery] string searchString)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var pagedItemListDtos = 
                await _itemListService.GetAllByRouteIdAsync(routeId, searchString, paginationDto);

            var pagedItemListViewModels = _mapper.Map< PagedList<ItemListViewModel> >(pagedItemListDtos);

            return Ok(pagedItemListViewModels);
        }

        /// <summary>
        /// Creates a new Item list.
        /// </summary>
        /// <param name="createItemListViewModel">Data for creating Item list.</param>
        /// <returns>Item list view model that
        /// represents the new entry that was added to database.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /item-lists
        ///     {
        ///         "title": "New item list.",
        ///         "routeId": 1
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(ItemListViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateItemListViewModel createItemListViewModel)
        {
            var createItemListDto = _mapper.Map<CreateItemListDto>(createItemListViewModel);

            var createdItemListDto = await _itemListService.CreateAsync(createItemListDto);

            var createdItemListViewModel = _mapper.Map<ItemListViewModel>(createdItemListDto);

            return Ok(createdItemListViewModel);
        }

        /// <summary>
        /// Updates Item list.
        /// </summary>
        /// <param name="updateItemListViewModel">New Item list data with
        /// existing Item list id.</param>
        /// <returns>Item list view model that
        /// represents the updated database entry.</returns>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /item-lists
        ///     {
        ///         "id": 1,
        ///         "title": "Updated title."
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(ItemListViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateItemListViewModel updateItemListViewModel)
        {
            var updateItemListDto = _mapper.Map<UpdateItemListDto>(updateItemListViewModel);

            var updatedItemListDto = await _itemListService.UpdateAsync(updateItemListDto);

            var updatedItemListViewModel = _mapper.Map<ItemListViewModel>(updatedItemListDto);

            return Ok(updatedItemListViewModel);
        }

        /// <summary>
        /// Deletes Item list.
        /// </summary>
        /// <param name="id">Id of Item list.</param>
        /// <returns>No content (HTTP code 204).</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            await _itemListService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
