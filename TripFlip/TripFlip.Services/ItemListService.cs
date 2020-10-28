using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Enums;
using TripFlip.Services.Helpers;
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

        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="currentUserService">ICurrentUserService instance.</param>
        public ItemListService(TripFlipDbContext tripFlipDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ItemListDto> GetByIdAsync(int id)
        {
            var itemListEntity = await _tripFlipDbContext
                .ItemLists
                .AsNoTracking()
                .SingleOrDefaultAsync(itemList => itemList.Id == id);

            EntityValidationHelper
                .ValidateEntityNotNull(itemListEntity, ErrorConstants.ItemListNotFound);

            var itemListDto = _mapper.Map<ItemListDto>(itemListEntity);

            return itemListDto;
        }

        public async Task<PagedList<ItemListDto>> GetAllByRouteIdAsync(int routeId,
            string searchString,
            PaginationDto paginationDto)
        {
            var routeExists = await _tripFlipDbContext.Routes
                .AnyAsync(r => r.Id == routeId);

            if (!routeExists)
            {
                throw new NotFoundException(ErrorConstants.RouteNotFound);
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

            var pagedItemListEntities = itemListEntitiesQuery.ToPagedList(pageNumber, pageSize);
            var pagedItemListDtos = _mapper.Map<PagedList<ItemListDto>>(pagedItemListEntities);

            return pagedItemListDtos;
        }

        public async Task<ItemListDto> CreateAsync(CreateItemListDto createItemListDto)
        {
            await ValidateRouteExistsAsync(createItemListDto.RouteId);

            var itemListEntity = _mapper.Map<ItemListEntity>(createItemListDto);

            // Validate current user has route 'Admin' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: itemListEntity.RouteId,
                routeRoleToValidate: RouteRoles.Admin,
                errorMessage: ErrorConstants.NotRouteAdmin);

            var entityEntry = _tripFlipDbContext.ItemLists.Add(itemListEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var itemListDto = _mapper.Map<ItemListDto>(entityEntry.Entity);

            return itemListDto;
        }

        public async Task<ItemListDto> UpdateAsync(UpdateItemListDto updateItemListDto)
        {
            var itemListEntity = await _tripFlipDbContext
                .ItemLists
                .SingleOrDefaultAsync(itemList => itemList.Id == updateItemListDto.Id);

            EntityValidationHelper
                .ValidateEntityNotNull(itemListEntity, ErrorConstants.ItemListNotFound);

            // Validate current user has route 'Editor' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: itemListEntity.RouteId,
                routeRoleToValidate: RouteRoles.Editor,
                errorMessage: ErrorConstants.NotRouteEditor);

            itemListEntity.Title = updateItemListDto.Title;

            await _tripFlipDbContext.SaveChangesAsync();

            var itemListDto = _mapper.Map<ItemListDto>(itemListEntity);

            return itemListDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var itemListEntity = await _tripFlipDbContext
                .ItemLists
                .SingleOrDefaultAsync(entity => entity.Id == id);

            EntityValidationHelper
                .ValidateEntityNotNull(itemListEntity, ErrorConstants.ItemListNotFound);

            // Validate current user has route 'Admin' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: itemListEntity.RouteId,
                routeRoleToValidate: RouteRoles.Admin,
                errorMessage: ErrorConstants.NotRouteAdmin);

            _tripFlipDbContext.ItemLists.Remove(itemListEntity);
            await _tripFlipDbContext.SaveChangesAsync();
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
                .SingleOrDefaultAsync(route => routeId == route.Id);

            EntityValidationHelper
                .ValidateEntityNotNull(routeEntity, ErrorConstants.RouteNotFound);
        }
    }
}
