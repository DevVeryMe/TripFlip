using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO.ItemDtos;

namespace TripFlip.Services.Interfaces
{
    public interface IItemService
    {
        /// <summary>
        /// Creates new item.
        /// </summary>
        /// <param name="createItemDto">new Item DTO</param>
        /// <returns>created item DTO</returns>
        Task<ItemDto> CreateAsync(CreateItemDto createItemDto);

        /// <summary>
        /// Returns all items for certain item list.
        /// </summary>
        /// <param name="listId">Id of item list</param>
        /// <returns>IEnumerable with item DTOs</returns>
        Task<IEnumerable<ItemDto>> GetAllAsync(int listId);
    }
}
