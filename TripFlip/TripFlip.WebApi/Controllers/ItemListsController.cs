using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TripFlip.ViewModels.ItemListViewModels;
using TripFlip.Services.Interfaces;
using TripFlip.Services.DTO.ItemListDtos;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/item-lists")]
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
        /// <returns>If operation is successful, returns <see cref="List{ResultItemListViewModel}"/> object that represents the list of database entries with the given Route Id.</returns>
        [HttpGet("route/{id}")]
        public async Task<IActionResult> GetAllByRouteIdAsync(int id)
        {
            var resultItemListDtos = await _itemListService.GetAllByRouteIdAsync(id);

            var resultItemListViewModelList = _mapper.Map< List<ResultItemListViewModel> >(resultItemListDtos);

            return Ok(resultItemListViewModelList);
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
