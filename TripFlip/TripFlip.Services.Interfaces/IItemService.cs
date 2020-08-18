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
        /// <param name="createItemDto">item data</param>
        /// <returns>created item</returns>
        Task<ItemDto> CreateAsync(CreateItemDto createItemDto);

        /// <summary>
        /// Returns all items for certain item list.
        /// </summary>
        /// <param name="listId">id of item list</param>
        /// <returns>IEnumerable with items</returns>
        Task<IEnumerable<ItemDto>> GetAllAsync(int listId);
    }
}
