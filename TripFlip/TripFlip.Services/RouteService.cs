using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Enums;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class RouteService : IRouteService
    {
        private readonly IMapper _mapper;

        private readonly TripFlipDbContext _tripFlipDbContext;

        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="currentUserService">ICurrentUserService instance.</param>
        public RouteService(TripFlipDbContext tripFlipDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<RouteDto> CreateAsync(CreateRouteDto createRouteDto)
        {
            await ValidateTripExistsAsync(createRouteDto.TripId);

            await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
               _currentUserService,
               _tripFlipDbContext,
               createRouteDto.TripId,
               TripRoles.Admin,
               ErrorConstants.NotTripAdmin);

            var routeEntity = _mapper.Map<RouteEntity>(createRouteDto);

            var entityEntry = _tripFlipDbContext.Routes.Add(routeEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var routeDto = _mapper.Map<RouteDto>(entityEntry.Entity);

            return routeDto;
        }

        public async Task<RouteDto> UpdateAsync(UpdateRouteDto updateRouteDto)
        {
            var tripEntity = await _tripFlipDbContext
                .Trips
                .Where(trip => trip.Id == updateRouteDto.TripId)
                .Include(trip => trip.Routes)
                .FirstOrDefaultAsync();

            EntityValidationHelper
                .ValidateEntityNotNull(tripEntity, ErrorConstants.TripNotFound);

            await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
               _currentUserService,
               _tripFlipDbContext,
               updateRouteDto.TripId,
               TripRoles.Editor,
               ErrorConstants.NotTripEditor);

            var routeEntity = tripEntity
                .Routes
                .FirstOrDefault(route => route.Id == updateRouteDto.Id);

            EntityValidationHelper
                .ValidateEntityNotNull(routeEntity, ErrorConstants.RouteNotFound);

            routeEntity.Title = updateRouteDto.Title;
            routeEntity.TripId = updateRouteDto.TripId;

            await _tripFlipDbContext.SaveChangesAsync();

            var routeDto = _mapper.Map<RouteDto>(routeEntity);

            return routeDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var routeEntity = await _tripFlipDbContext
                .Routes
                .Include(route => route.ItemLists)
                .Include(route => route.TaskLists)
                .SingleOrDefaultAsync(entity => entity.Id == id);

            EntityValidationHelper
                .ValidateEntityNotNull(routeEntity, ErrorConstants.RouteNotFound);

            await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
               _currentUserService,
               _tripFlipDbContext,
               routeEntity.TripId,
               TripRoles.Admin,
               ErrorConstants.NotTripAdmin);

            _tripFlipDbContext.ItemLists.RemoveRange(routeEntity.ItemLists);
            _tripFlipDbContext.TaskLists.RemoveRange(routeEntity.TaskLists);

            _tripFlipDbContext.Routes.Remove(routeEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task<RouteDto> GetByIdAsync(int id)
        {
            var routeEntity = await _tripFlipDbContext
                .Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(route => route.Id == id);

            EntityValidationHelper
                .ValidateEntityNotNull(routeEntity, ErrorConstants.RouteNotFound);

            var routeDto = _mapper.Map<RouteDto>(routeEntity);

            return routeDto;
        }

        public async Task<PagedList<RouteDto>> GetAllByTripIdAsync(int tripId,
            string searchString,
            PaginationDto paginationDto)
        {
            await ValidateTripExistsAsync(tripId);

            var routeEntitiesQuery = _tripFlipDbContext
                .Routes
                .AsNoTracking()
                .Where(r => r.TripId == tripId);

            if (!string.IsNullOrEmpty(searchString))
            {
                routeEntitiesQuery = routeEntitiesQuery
                    .Where(routeEntity => routeEntity.Title.Contains(searchString));
            }

            int pageNumber = paginationDto.PageNumber ?? 1;
            int pageSize = paginationDto.PageSize ?? await routeEntitiesQuery.CountAsync();

            var pagedRouteEntities = routeEntitiesQuery
                .ToPagedList(pageNumber, pageSize);

            var pagedRouteDtos = _mapper
                .Map<PagedList<RouteDto>>(pagedRouteEntities);

            return pagedRouteDtos;
        }

        /// <summary>
        /// Checks if Trip exists by making a database query.
        /// </summary>
        async Task ValidateTripExistsAsync(int tripId)
        {
            var tripEntity = await _tripFlipDbContext
                .Trips
                .AsNoTracking()
                .SingleOrDefaultAsync(trip => tripId == trip.Id);

            EntityValidationHelper.ValidateEntityNotNull(tripEntity, ErrorConstants.TripNotFound);
        }
    }
}
