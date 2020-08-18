﻿using System.Collections.Generic;
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

        /// <summary>
        /// Updates existing item.
        /// </summary>
        /// <param name="itemDto">item data</param>
        /// <returns>updated item</returns>
        Task<ItemDto> UpdateAsync(ItemDto itemDto);

        /// <summary>
        /// Gets item by id.
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns>item dto</returns>
        Task<ItemDto> GetByIdAsync(int id);

        /// <summary>
        /// Deletes item by id.
        /// </summary>
        /// <param name="id">item id</param>
        Task DeleteAsync(int id);
    }
}
