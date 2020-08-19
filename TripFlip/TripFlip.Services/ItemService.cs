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
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var itemEntity = _mapper.Map<ItemEntity>(createItemDto);

            await _flipTripDbContext.Items.AddAsync(itemEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var itemToReturnDto = _mapper.Map<ItemDto>(itemEntity);

            return itemToReturnDto;
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync(int listId)
        {
            var itemEntityList = await _flipTripDbContext.ItemLists.AsNoTracking().
                SingleOrDefaultAsync(l => l.Id == listId);

            if (itemEntityList is null)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var itemEntities = await _flipTripDbContext.Items.
                Where(i => i.ItemListId == listId).
                AsNoTracking().ToListAsync();

            var itemDtos = _mapper.Map<List<ItemDto>>(itemEntities);

            return itemDtos;
        }
    }
}
