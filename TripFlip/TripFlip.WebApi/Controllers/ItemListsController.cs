using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.ItemListDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels;
using TripFlip.ViewModels.ItemListViewModels;

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
        /// <param name="id">Id of ItemList.</param>
        /// <returns>If operation is successful, returns <see cref="ResultItemListViewModel"/> object that represents ItemList database entry.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var resultRouteDto = await _itemListService.GetByIdAsync(id);

            var resultRouteViewModel = _mapper.Map<ResultItemListViewModel>(resultRouteDto);

            return Ok(resultRouteViewModel);
        }

        /// <summary>
        /// Returns all ItemLists with the given Route Id.
        /// </summary>
        /// <param name="id">Id of Route.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <param name="searchString">Search string to filter data.</param>
        /// <returns>If operation is successful, returns <see cref="PagedList{ResultItemListViewModel}"/> object that represents the list of database entries with the given Route Id.</returns>
        [HttpGet("route/{id}")]
        public async Task<IActionResult> GetAllByRouteIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int id, 
            [FromQuery] PaginationViewModel paginationViewModel,
            [FromQuery] string searchString)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var pagedListOfItemListDtos = 
                await _itemListService.GetAllByRouteIdAsync(id, paginationDto, searchString);

            var pagedListOfItemListViewModels = _mapper.Map< PagedList<ResultItemListViewModel> >(pagedListOfItemListDtos);

            return Ok(pagedListOfItemListViewModels);
        }

        /// <summary>
        /// Creates a new ItemList with the given <see cref="CreateItemListViewModel"/> object.
        /// </summary>
        /// <param name="createItemListViewModel">ViewModel that represents new ItemList to be created.</param>
        /// <returns>If operation is successful, returns <see cref="ResultItemListViewModel"/> object that represents the new entry that was added to database.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateItemListViewModel createItemListViewModel)
        {
            var createItemListDto = _mapper.Map<CreateItemListDto>(createItemListViewModel);

            var resultItemListDto = await _itemListService.CreateAsync(createItemListDto);

            var resultItemListViewModel = _mapper.Map<ResultItemListViewModel>(resultItemListDto);

            return Ok(resultItemListViewModel);
        }

        /// <summary>
        /// Updates ItemList with the given <see cref="UpdateItemListViewModel"/> object.
        /// </summary>
        /// <param name="updateItemListViewModel">ViewModel that represents changes in ItemList to be made.</param>
        /// <returns>If operation is successful, returns <see cref="ResultItemListDto"/> object that represents the updated database entry.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateItemListViewModel updateItemListViewModel)
        {
            var updateItemListDto = _mapper.Map<UpdateItemListDto>(updateItemListViewModel);

            var resultItemListDto = await _itemListService.UpdateAsync(updateItemListDto);

            var resultItemListViewModel = _mapper.Map<ResultItemListViewModel>(resultItemListDto);

            return Ok(resultItemListViewModel);
        }

        /// <summary>
        /// Deletes ItemList with the given Id.
        /// </summary>
        /// <param name="id">Id of ItemList to be deleted.</param>
        /// <returns>If operation is successful, returns NoContent (HTTP code 204) result.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _itemListService.DeleteAsync(id);

            return NoContent();
        }
    }
}
