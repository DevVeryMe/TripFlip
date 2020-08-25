using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.ItemDtos;
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
        /// Initializes _tripFlipDbContext and _mapper fields.
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
                .AsNoTracking().SingleOrDefaultAsync(itemList => createItemDto.ItemListId == itemList.Id);

            if (itemListEntity == null)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var itemEntity = _mapper.Map<ItemEntity>(createItemDto);

            await _tripFlipDbContext.Items.AddAsync(itemEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var itemToReturnDto = _mapper.Map<ItemDto>(itemEntity);

            return itemToReturnDto;
        }

        public async Task<PagedList<ItemDto>> GetAllAsync(int listId, PaginationDto paginationDto, string searchString)
        {
            var itemListExists = await _tripFlipDbContext.ItemLists
                .AnyAsync(l => l.Id == listId);

            if (!itemListExists)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }

            var itemEntitiesQuery = _tripFlipDbContext.Items
                .Where(i => i.ItemListId == listId)
                .AsNoTracking();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                itemEntitiesQuery = itemEntitiesQuery
                    .Where(i => i.Title.Contains(searchString) || i.Comment.Contains(searchString));
            }

            var pageNumber = paginationDto.PageNumber ?? 1;
            var pageSize = paginationDto.PageSize ?? await itemEntitiesQuery.CountAsync();

            var pagedListOfItemEntities = itemEntitiesQuery.ToPagedList(pageNumber, pageSize);
            var pagedListOfItemDtos = _mapper.Map<PagedList<ItemDto>>(pagedListOfItemEntities);

            return pagedListOfItemDtos;
        }

        public async Task<ItemDto> UpdateAsync(UpdateItemDto itemDto)
        {
            var itemToUpdate = await _tripFlipDbContext.Items.FindAsync(itemDto.Id);

            ValidateItemEntityIsNotNull(itemToUpdate);

            itemToUpdate.Title = itemDto.Title;
            itemToUpdate.Comment = itemDto.Comment;
            itemToUpdate.Quantity = itemDto.Quantity;
            itemToUpdate.IsCompleted = itemDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var itemDtoToReturn = _mapper.Map<ItemDto>(itemToUpdate);

            return itemDtoToReturn;
        }

        public async Task<ItemDto> UpdateCompletenessAsync(UpdateItemCompletenessDto updateItemCompletenessDto)
        {
            var itemToUpdate = await _tripFlipDbContext.Items
                .FindAsync(updateItemCompletenessDto.Id);

            ValidateItemEntityIsNotNull(itemToUpdate);

            itemToUpdate.IsCompleted = updateItemCompletenessDto.IsCompleted;
            
            await _tripFlipDbContext.SaveChangesAsync();
            var itemDtoToReturn = _mapper.Map<ItemDto>(itemToUpdate);

            return itemDtoToReturn;
        }

        public async Task<ItemDto> GetByIdAsync(int id)
        {
            var itemEntity = await _tripFlipDbContext.Items.AsNoTracking().
                SingleOrDefaultAsync(i => i.Id == id);

            ValidateItemEntityIsNotNull(itemEntity);

            var itemDtoToReturn = _mapper.Map<ItemDto>(itemEntity);

            return itemDtoToReturn;
        }

        public async Task DeleteAsync(int id)
        {
            var itemEntityToDelete = await _tripFlipDbContext.Items.FindAsync(id);

            ValidateItemEntityIsNotNull(itemEntityToDelete);

            _tripFlipDbContext.Remove(itemEntityToDelete);
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
