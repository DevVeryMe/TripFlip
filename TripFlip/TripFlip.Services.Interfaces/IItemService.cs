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
        Task<IEnumerable<CreateItemDto>> GetAllAsync(int itemListId);

        /// <summary>
        /// Gets item by id.
        /// </summary>
        /// <param name="id">item id</param>
        Task<CreateItemDto> GetAsync(int id);

        /// <summary>
        /// Creates new item.
        /// </summary>
        /// <param name="itemDto">item data</param>
        /// <returns>created item</returns>
        Task<CreateItemDto> CreateAsync(CreateItemDto itemDto);

        /// <summary>
        /// Updates existing item.
        /// </summary>
        /// <param name="itemDto">new item data</param>
        /// <returns>updated item</returns>
        Task<CreateItemDto> UpdateAsync(CreateItemDto itemDto);

        /// <summary>
        /// Deletes item.
        /// </summary>
        /// <param name="id">item to delete id</param>
        Task DeleteAsync(int id);
    }
}
