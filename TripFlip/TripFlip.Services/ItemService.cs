using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly FlipTripDbContext _flipTripDbContext;

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
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            var item = _mapper.Map<ItemEntity>(createItemDto);

            await _flipTripDbContext.Items.AddAsync(item);
            await _flipTripDbContext.SaveChangesAsync();

            var itemToReturnDto = _mapper.Map<ItemDto>(item);

            return itemToReturnDto;
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync(int listId)
        {
            var itemList = await _flipTripDbContext.ItemLists.AsNoTracking().
                SingleOrDefaultAsync(l => l.Id == listId);

            if (itemList is null)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var items = await _flipTripDbContext.Items.
                Where(i => i.ItemListId == listId).
                AsNoTracking().ToListAsync();

            var itemDtos = _mapper.Map<List<ItemDto>>(items);

            return itemDtos;
        }

        public async Task<ItemDto> UpdateAsync(ItemDto itemDto)
        {
            var itemToUpdate = await _flipTripDbContext.Items.FindAsync(itemDto.Id);

            if (itemToUpdate is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            itemToUpdate.Title = itemDto.Title;
            itemToUpdate.Comment = itemDto.Comment;
            itemToUpdate.Quantity = itemDto.Quantity;
            itemToUpdate.IsCompleted = itemDto.IsCompleted;

            await _flipTripDbContext.SaveChangesAsync();
            var newTripDto = _mapper.Map<ItemDto>(itemToUpdate);

            return newTripDto;
        }
    }
}
