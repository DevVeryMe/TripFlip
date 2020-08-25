using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    public interface IItemService
    {
        /// <summary>
        /// Creates new item.
        /// </summary>
        /// <param name="createItemDto">New Item DTO.</param>
        /// <returns>Created item DTO.</returns>
        Task<ItemDto> CreateAsync(CreateItemDto createItemDto);

        /// <summary>
        /// Returns all items for certain item list.
        /// </summary>
        /// <param name="itemListId">Id of item list.</param>
        /// <param name="paginationDto">Pagination settings.</param>
        /// <param name="searchString">Search string to filter data.</param>
        /// <returns>Paged list with item DTOs.</returns>
        Task<PagedList<ItemDto>> GetAllByItemListIdAsync(int itemListId,
            string searchString, PaginationDto paginationDto);

        /// <summary>
        /// Updates existing item.
        /// </summary>
        /// <param name="itemDto">Item DTO.</param>
        /// <returns>Updated item DTO.</returns>
        Task<ItemDto> UpdateAsync(UpdateItemDto updateItemDto);

        /// <summary>
        /// Updates existing item completeness.
        /// </summary>
        /// <param name="updateItemCompletenessDto">New item data with field that
        /// determines is item completed.</param>
        /// <returns>Updated item DTO.</returns>
        Task<ItemDto> UpdateCompletenessAsync(UpdateItemCompletenessDto updateItemCompletenessDto);

        /// <summary>
        /// Gets item by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>Item DTO.</returns>
        Task<ItemDto> GetByIdAsync(int id);

        /// <summary>
        /// Deletes item by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        Task DeleteByIdAsync(int id);
    }
}
