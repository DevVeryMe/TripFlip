using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class ItemListService : IItemListService
    {
        private readonly IMapper _mapper;

        private readonly TripFlipDbContext _tripFlipDbContext;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        public ItemListService(TripFlipDbContext tripFlipDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
        }

        public async Task<ResultItemListDto> GetByIdAsync(int id)
        {
            var itemListEntity = await _tripFlipDbContext
                .ItemLists
                .AsNoTracking()
                .SingleOrDefaultAsync(itemListEntity => itemListEntity.Id == id);

            ValidateItemListEntityIsNotNull(itemListEntity);

            var resultItemListDto = _mapper.Map<ResultItemListDto>(itemListEntity);

            return resultItemListDto;
        }

        public async Task<PagedList<ResultItemListDto>> GetAllByRouteIdAsync(int routeId,
            string searchString,
            PaginationDto paginationDto)
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

        public async Task DeleteByIdAsync(int id)
        {
            var itemListEntityToDelete = await _tripFlipDbContext
                .ItemLists
                .SingleOrDefaultAsync(itemListEntity => itemListEntity.Id == id);

            ValidateItemListEntityIsNotNull(itemListEntityToDelete);

            _tripFlipDbContext.ItemLists.Remove(itemListEntityToDelete);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the given <see cref="ItemListEntity"/> is not null. If null,
        /// then throws an <see cref="ArgumentException"/> with a corresponding message.
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
        /// <param name="routeId">Route's id to check.</param>
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
