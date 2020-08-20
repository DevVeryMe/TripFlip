using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Interfaces;
using TripFlip.Services.DTO.ItemListDtos;

namespace TripFlip.Services
{
    /// <summary>
    /// Class that performs CRUD operations related to <see cref="ItemListEntity"/>.
    /// </summary>
    public class ItemListService : IITemListService
    {
        private readonly IMapper _mapper;
        private readonly FlipTripDbContext _flipTripDbContext;

        public ItemListService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _flipTripDbContext = flipTripDbContext;
        }

        public async Task<ResultItemListDto> GetByIdAsync(int itemListId)
        {
            var itemListEntity = await _flipTripDbContext
                .ItemLists
                .AsNoTracking()
                .SingleOrDefaultAsync(itemListEntity => itemListEntity.Id == itemListId);

            ValidateItemListEntityIsNotNull(itemListEntity);

            var resultItemListDto = _mapper.Map<ResultItemListDto>(itemListEntity);

            return resultItemListDto;
        }

        public async Task<IEnumerable<ResultItemListDto>> GetAllByRouteIdAsync(int routeId)
        {
            var routeEntity = await _flipTripDbContext
                .Routes
                .AsNoTracking()
                .Include(routeEntity => routeEntity.ItemLists)
                .SingleOrDefaultAsync(routeEntity => routeEntity.Id == routeId);

            ValidateRouteEntityIsNotNull(routeEntity);

            var resultRouteDtoList = _mapper.Map< List<ResultItemListDto> >
                (routeEntity.ItemLists.ToList());

            return resultRouteDtoList;
        }

        public async Task<ResultItemListDto> CreateAsync(CreateItemListDto createItemListDto)
        {
            await ValidateRouteExistsAsync(createItemListDto.RouteId);

            var itemListEntity = _mapper.Map<ItemListEntity>(createItemListDto);

            itemListEntity.DateCreated = DateTimeOffset.Now;

            var entityEntry = _flipTripDbContext.ItemLists.Add(itemListEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var resultItemListDto = _mapper.Map<ResultItemListDto>(entityEntry.Entity);

            return resultItemListDto;
        }

        public async Task<ResultItemListDto> UpdateAsync(UpdateItemListDto updateItemListDto)
        {
            var itemListEntity = await _flipTripDbContext
                .ItemLists
                .SingleOrDefaultAsync(itemListEntity => itemListEntity.Id == updateItemListDto.Id);

            ValidateItemListEntityIsNotNull(itemListEntity);

            itemListEntity.Title = updateItemListDto.Title;

            await _flipTripDbContext.SaveChangesAsync();

            var resultItemListDto = _mapper.Map<ResultItemListDto>(itemListEntity);

            return resultItemListDto;
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
            var routeEntity = await _flipTripDbContext
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
