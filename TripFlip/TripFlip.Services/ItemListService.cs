using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.DTO.ItemListDtos;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;

namespace TripFlip.Services
{
    /// <summary>
    /// Class that performs CRUD operations related to <see cref="ItemListEntity"/>.
    /// </summary>
    public class ItemListService : IITemListService
    {
        private readonly IMapper _mapper;
        private readonly TripFlipDbContext _tripFlipDbContext;

        public ItemListService(TripFlipDbContext tripFlipDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
        }

        public async Task<ResultItemListDto> GetByIdAsync(int itemListId)
        {
            var itemListEntity = await _tripFlipDbContext
                .ItemLists
                .AsNoTracking()
                .SingleOrDefaultAsync(itemListEntity => itemListEntity.Id == itemListId);

            ValidateItemListEntityIsNotNull(itemListEntity);

            var resultItemListDto = _mapper.Map<ResultItemListDto>(itemListEntity);

            return resultItemListDto;
        }

        public async Task<PagedList<ResultItemListDto>> GetAllByRouteIdAsync(int routeId, 
            PaginationDto paginationDto, string searchString)
        {
            var routeExists = await _tripFlipDbContext.Routes
                .AnyAsync(r => r.Id == routeId);

            if (!routeExists)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }

            var itemListEntitiesQuery = _tripFlipDbContext.ItemLists
                .Where(l => l.RouteId == routeId)
                .AsNoTracking();
            
            if(!string.IsNullOrEmpty(searchString))
            {
                itemListEntitiesQuery = itemListEntitiesQuery
                    .Where(i => i.Title.Contains(searchString));
            }

            var pageNumber = paginationDto.PageNumber ?? 1;
            var pageSize = paginationDto.PageSize ?? await itemListEntitiesQuery.CountAsync();

            var pagedListOfItemListEntities = itemListEntitiesQuery.ToPagedList(pageNumber, pageSize);
            var pagedListOfItemListDtos = _mapper.Map<PagedList<ResultItemListDto>>(pagedListOfItemListEntities);

            return pagedListOfItemListDtos;
        }

        public async Task<ResultItemListDto> CreateAsync(CreateItemListDto createItemListDto)
        {
            await ValidateRouteExistsAsync(createItemListDto.RouteId);

            var itemListEntity = _mapper.Map<ItemListEntity>(createItemListDto);

            var entityEntry = _tripFlipDbContext.ItemLists.Add(itemListEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var resultItemListDto = _mapper.Map<ResultItemListDto>(entityEntry.Entity);

            return resultItemListDto;
        }

        public async Task<ResultItemListDto> UpdateAsync(UpdateItemListDto updateItemListDto)
        {
            var itemListEntity = await _tripFlipDbContext
                .ItemLists
                .SingleOrDefaultAsync(itemListEntity => itemListEntity.Id == updateItemListDto.Id);

            ValidateItemListEntityIsNotNull(itemListEntity);

            itemListEntity.Title = updateItemListDto.Title;

            await _tripFlipDbContext.SaveChangesAsync();

            var resultItemListDto = _mapper.Map<ResultItemListDto>(itemListEntity);

            return resultItemListDto;
        }

        public async Task DeleteAsync(int id)
        {
            var itemListEntityToDelete = await _tripFlipDbContext
                .ItemLists
                .SingleOrDefaultAsync(itemListEntity => itemListEntity.Id == id);

            ValidateItemListEntityIsNotNull(itemListEntityToDelete);

            _tripFlipDbContext.ItemLists.Remove(itemListEntityToDelete);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the given <see cref="RouteEntity"/> is not null. If null, then throws an <see cref="ArgumentException"/> with corresponding message.
        /// </summary>
        /// <param name="routeEntity">Object that should be checked.</param>
        void ValidateRouteEntityIsNotNull(RouteEntity routeEntity)
        {
            if (routeEntity == null)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }
        }

        /// <summary>
        /// Checks if the given <see cref="ItemListEntity"/> is not null. If null, then throws an <see cref="ArgumentException"/> with corresponding message.
        /// </summary>
        /// <param name="itemListEntity">Object that should be checked.</param>
        void ValidateItemListEntityIsNotNull(ItemListEntity itemListEntity)
        {
            if (itemListEntity == null)
            {
                throw new ArgumentException(ErrorConstants.ItemListNotFound);
            }
        }

        /// <summary>
        /// Checks if Route exists by making a database query.
        /// </summary>
        /// <param name="routeId">Route's Id to check.</param>
        async Task ValidateRouteExistsAsync(int routeId)
        {
            var routeEntity = await _tripFlipDbContext
                .Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(routeEntity => routeId == routeEntity.Id);

            if (routeEntity == null)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }
        }
    }
}
