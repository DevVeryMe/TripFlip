﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO.ItemDtos;

namespace TripFlip.Services.Interfaces
{
    public interface IItemService
    {
        /// <summary>
        /// Gets all items of certain item list.
        /// </summary>
        Task<IEnumerable<ItemDto>> GetAllAsync(int itemListId);

        /// <summary>
        /// Gets item by id.
        /// </summary>
        /// <param name="id">item id</param>
        Task<ItemDto> GetAsync(int id);

        /// <summary>
        /// Creates new item.
        /// </summary>
        /// <param name="createItemDto">item data</param>
        /// <returns>created item</returns>
        Task<ItemDto> CreateAsync(CreateItemDto createItemDto);

        /// <summary>
        /// Updates existing item.
        /// </summary>
        /// <param name="createItemDto">new item data</param>
        /// <returns>updated item</returns>
        Task<ItemDto> UpdateAsync(CreateItemDto createItemDto);

        /// <summary>
        /// Deletes item.
        /// </summary>
        /// <param name="id">item to delete id</param>
        Task DeleteAsync(int id);
    }
}
