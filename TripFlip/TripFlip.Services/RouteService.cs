using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.RouteDtos;
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

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        public RouteService(TripFlipDbContext tripFlipDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
        }

        public async Task<RouteDto> CreateAsync(CreateRouteDto createRouteDto)
        {
            await ValidateTripExistsAsync(createRouteDto.TripId);

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
                .Where(tripEntity => tripEntity.Id == updateRouteDto.TripId)
                .Include(tripEntity => tripEntity.Routes)
                .FirstOrDefaultAsync();

            ValidateTripEntityIsNotNull(tripEntity);

            var routeEntity = tripEntity
                .Routes
                .FirstOrDefault(routeEntity => routeEntity.Id == updateRouteDto.Id);

            ValidateRouteEntityIsNotNull(routeEntity);

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
                .SingleOrDefaultAsync(entity => entity.Id == id);

            ValidateRouteEntityIsNotNull(routeEntity);

            _tripFlipDbContext.Routes.Remove(routeEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task<RouteDto> GetByIdAsync(int id)
        {
            var routeEntity = await _tripFlipDbContext
                .Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(routeEntity => routeEntity.Id == id);

            ValidateRouteEntityIsNotNull(routeEntity);

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
        /// Checks if the given <see cref="TripEntity"/> is not null. If null,
        /// then throws an <see cref="NotFoundException"/> with a corresponding message.
        /// </summary>
        /// <param name="tripEntity">Object that should be checked.</param>
        void ValidateTripEntityIsNotNull(TripEntity tripEntity)
        {

            if (tripEntity == null)
            {
                throw new NotFoundException(ErrorConstants.TripNotFound);
            }

        }

        /// <summary>
        /// Checks if the given <see cref="RouteEntity"/> is not null. If null,
        /// then throws an <see cref="NotFoundException"/> with a corresponding message.
        /// </summary>
        /// <param name="routeEntity">Object that should be checked.</param>
        void ValidateRouteEntityIsNotNull(RouteEntity routeEntity)
        {

            if (routeEntity == null)
            {
                throw new NotFoundException(ErrorConstants.RouteNotFound);
            }

        }

        /// <summary>
        /// Checks if Trip exists by making a database query.
        /// </summary>
        async Task ValidateTripExistsAsync(int tripId)
        {
            var tripEntity = await _tripFlipDbContext
                .Trips
                .AsNoTracking()
                .SingleOrDefaultAsync(tripEntity => tripId == tripEntity.Id);

            if (tripEntity == null)
            {
                throw new NotFoundException(ErrorConstants.TripNotFound);
            }

        }
    }
}
