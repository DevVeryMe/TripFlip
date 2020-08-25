using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;

        private readonly TripFlipDbContext _tripFlipDbContext;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        public ItemService(IMapper mapper, TripFlipDbContext tripFlipDbContext)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
        }

        public async Task<ItemDto> CreateAsync(CreateItemDto createItemDto)
        {
            var itemListEntity = await _tripFlipDbContext.ItemLists
                .AsNoTracking()
                .SingleOrDefaultAsync(itemList => createItemDto.ItemListId == itemList.Id);

            if (itemListEntity == null)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var itemEntity = _mapper.Map<ItemEntity>(createItemDto);

            await _tripFlipDbContext.Items.AddAsync(itemEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var itemDto = _mapper.Map<ItemDto>(itemEntity);

            return itemDto;
        }

        public async Task<PagedList<ItemDto>> GetAllByItemListIdAsync(int itemListId,
            string searchString,
            PaginationDto paginationDto)
        {
            var itemListExists = await _tripFlipDbContext.ItemLists
                .AnyAsync(l => l.Id == itemListId);

            if (!itemListExists)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var itemEntitiesQuery = _tripFlipDbContext.Items
                .Where(i => i.ItemListId == itemListId)
                .AsNoTracking();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                itemEntitiesQuery = itemEntitiesQuery
                    .Where(i => i.Title.Contains(searchString) || i.Comment.Contains(searchString));
            }

            var pageNumber = paginationDto.PageNumber ?? 1;
            var pageSize = paginationDto.PageSize ?? await itemEntitiesQuery.CountAsync();

            var pagedItemEntities = itemEntitiesQuery.ToPagedList(pageNumber, pageSize);
            var pagedItemDtos = _mapper.Map<PagedList<ItemDto>>(pagedItemEntities);

            return pagedItemDtos;
        }

        public async Task<ItemDto> UpdateAsync(UpdateItemDto updateItemDto)
        {
            var itemEntity = await _tripFlipDbContext.Items
                .FindAsync(updateItemDto.Id);

            ValidateItemEntityIsNotNull(itemEntity);

            itemEntity.Title = updateItemDto.Title;
            itemEntity.Comment = updateItemDto.Comment;
            itemEntity.Quantity = updateItemDto.Quantity;
            itemEntity.IsCompleted = updateItemDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var itemDto = _mapper.Map<ItemDto>(itemEntity);

            return itemDto;
        }

        public async Task<ItemDto> UpdateCompletenessAsync(UpdateItemCompletenessDto updateItemCompletenessDto)
        {
            var itemEntity = await _tripFlipDbContext.Items
                .FindAsync(updateItemCompletenessDto.Id);

            ValidateItemEntityIsNotNull(itemEntity);

            itemEntity.IsCompleted = updateItemCompletenessDto.IsCompleted;
            
            await _tripFlipDbContext.SaveChangesAsync();
            var itemDto = _mapper.Map<ItemDto>(itemEntity);

            return itemDto;
        }

        public async Task<ItemDto> GetByIdAsync(int id)
        {
            var itemEntity = await _tripFlipDbContext.Items.AsNoTracking()
                .SingleOrDefaultAsync(i => i.Id == id);

            ValidateItemEntityIsNotNull(itemEntity);

            var itemDto = _mapper.Map<ItemDto>(itemEntity);

            return itemDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var itemEntity = await _tripFlipDbContext.Items.FindAsync(id);

            ValidateItemEntityIsNotNull(itemEntity);

            _tripFlipDbContext.Remove(itemEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        private void ValidateItemEntityIsNotNull(ItemEntity itemEntity)
        {

            if (itemEntity is null)
            {
                throw new ArgumentException(ErrorConstants.ItemNotFound);
            }

        }
    }
}
