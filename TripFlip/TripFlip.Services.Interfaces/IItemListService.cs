using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Interface that describes supported CRUD operations with Item list entity.
    /// </summary>
    public interface IItemListService
    {
        /// <summary>
        /// Gets Item list with the given Id.
        /// </summary>
        /// <param name="id">Id of Item list.</param>
        /// <returns>Item list DTO that represents the database entry with the given id.</returns>
        Task<ResultItemListDto> GetByIdAsync(int id);

        /// <summary>
        /// Gets all Item lists with the given Route id.
        /// </summary>
        /// <param name="routeId">Id of Route.</param>
        /// <param name="paginationDto">Pagination settings.</param>
        /// <param name="searchString">String to filter Item lists.</param>
        /// <returns>Paged list of Item list DTOs that represent the database entries.</returns>
        Task<PagedList<ResultItemListDto>> GetAllByRouteIdAsync(int routeId,
            string searchString,
            PaginationDto paginationDto);

        /// <summary>
        /// Creates a new Item list.
        /// </summary>
        /// <param name="createItemListDto">Data to create a new Item list.</param>
        /// <returns>Item list DTO that represents the new entry that was added to database.</returns>
        Task<ResultItemListDto> CreateAsync(CreateItemListDto createItemListDto);

        /// <summary>
        /// Updates the Item list.
        /// </summary>
        /// <param name="updateItemListDto">New Item list data with existing Item list id.</param>
        /// <returns>Item list DTO that represents the updated database entry.</returns>
        Task<ResultItemListDto> UpdateAsync(UpdateItemListDto updateItemListDto);

        /// <summary>
        /// Deletes Item list.
        /// </summary>
        /// <param name="id">Id of Item list.</param>
        Task DeleteByIdAsync(int id);
    }
}
