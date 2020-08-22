using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.HelpersExtensions;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly FlipTripDbContext _flipTripDbContext;

        /// <summary>
        /// Initializes _flipTripDbContext and _mapper fields.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="flipTripDbContext">FlipTripDbContext instance.</param>
        public ItemService(IMapper mapper, FlipTripDbContext flipTripDbContext)
        {
            _mapper = mapper;
            _flipTripDbContext = flipTripDbContext;
        }

        public async Task<ItemDto> CreateAsync(CreateItemDto createItemDto)
        {
            var itemListEntity = await _flipTripDbContext.ItemLists
                .AsNoTracking().SingleOrDefaultAsync(itemList => createItemDto.ItemListId == itemList.Id);

            if (itemListEntity == null)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var itemEntity = _mapper.Map<ItemEntity>(createItemDto);

            await _flipTripDbContext.Items.AddAsync(itemEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var itemToReturnDto = _mapper.Map<ItemDto>(itemEntity);

            return itemToReturnDto;
        }

        public async Task<PagedList<ItemDto>> GetAllAsync(int listId, int pageNumber, int pageSize)
        {
            var itemListExists = await _flipTripDbContext.ItemLists
                .AnyAsync(l => l.Id == listId);

            if (!itemListExists)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var itemEntities = _flipTripDbContext.Items
                .Where(i => i.ItemListId == listId);

            var pagedListOfItemEntities = itemEntities.ToPagedList(pageNumber, pageSize);
            var pagedListOfItemDtos = _mapper.Map<PagedList<ItemDto>>(pagedListOfItemEntities);

            return pagedListOfItemDtos;
        }

        public async Task<ItemDto> UpdateAsync(UpdateItemDto itemDto)
        {
            var itemToUpdate = await _flipTripDbContext.Items.FindAsync(itemDto.Id);

            ValidateItemEntityExists(itemToUpdate);

            itemToUpdate.Title = itemDto.Title;
            itemToUpdate.Comment = itemDto.Comment;
            itemToUpdate.Quantity = itemDto.Quantity;
            itemToUpdate.IsCompleted = itemDto.IsCompleted;

            await _flipTripDbContext.SaveChangesAsync();
            var itemDtoToReturn = _mapper.Map<ItemDto>(itemToUpdate);

            return itemDtoToReturn;
        }

        public async Task<ItemDto> GetByIdAsync(int id)
        {
            var itemEntity = await _flipTripDbContext.Items.AsNoTracking().
                SingleOrDefaultAsync(i => i.Id == id);

            ValidateItemEntityExists(itemEntity);

            var itemDtoToReturn = _mapper.Map<ItemDto>(itemEntity);

            return itemDtoToReturn;
        }

        public async Task DeleteAsync(int id)
        {
            var itemEntityToDelete = await _flipTripDbContext.Items.FindAsync(id);

            ValidateItemEntityExists(itemEntityToDelete);

            _flipTripDbContext.Remove(itemEntityToDelete);
            await _flipTripDbContext.SaveChangesAsync();
        }

        private void ValidateItemEntityExists(ItemEntity itemEntity)
        {
            if (itemEntity is null)
            {
                throw new ArgumentException(ErrorConstants.ItemNotFound);
            }
        }
    }
}
