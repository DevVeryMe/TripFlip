using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Describes supported operations with Item entity.
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Creates a new Item.
        /// </summary>
        /// <param name="createItemDto">Data to create a new Item.</param>
        /// <returns>Item DTO that represents the new entry that was added to database.</returns>
        Task<ItemDto> CreateAsync(CreateItemDto createItemDto);

        /// <summary>
        /// Gets all Items with given Item list id.
        /// </summary>
        /// <param name="itemListId">Id of Item list.</param>
        /// <param name="paginationDto">Pagination settings.</param>
        /// <param name="searchString">String to filter data.</param>
        /// <returns>Paged list of Item DTOs that represent the database entries.</returns>
        Task<PagedList<ItemDto>> GetAllByItemListIdAsync(int itemListId,
            string searchString,
            PaginationDto paginationDto);

        /// <summary>
        /// Updates Item.
        /// </summary>
        /// <param name="updateItemDto">Updated Item data with existing Item id.</param>
        /// <returns>Item DTO that represents the updated database entry.</returns>
        Task<ItemDto> UpdateAsync(UpdateItemDto updateItemDto);

        /// <summary>
        /// Updates existing Item's completeness.
        /// </summary>
        /// <param name="updateItemCompletenessDto">New Item data with field that
        /// determines is Item completed and Item id.</param>
        /// <returns>Item DTO that represents the updated database entry.</returns>
        Task<ItemDto> UpdateCompletenessAsync(UpdateItemCompletenessDto updateItemCompletenessDto);

        /// <summary>
        /// Gets Item by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>Item DTO that represents the database entry.</returns>
        Task<ItemDto> GetByIdAsync(int id);

        /// <summary>
        /// Deletes Item.
        /// </summary>
        /// <param name="id">Item id.</param>
        Task DeleteByIdAsync(int id);

        /// <summary>
        /// If current user owns 'Editor' role,
        /// assigns given route subscribers to given item.
        /// </summary>
        /// <param name="itemAssigneesDto">DTO that contains 
        /// route subscribers IDs and item ID to assign them to.</param>
        Task SetItemAssigneesAsync(ItemAssigneesDto itemAssigneesDto);
    }
}
