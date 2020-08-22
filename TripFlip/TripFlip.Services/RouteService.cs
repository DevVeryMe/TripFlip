using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TripFlip.Services.Interfaces;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.HelpersExtensions;

namespace TripFlip.Services
{
    /// <summary>
    /// Class that performs CRUD operations related to <see cref="RouteEntity"/>.
    /// </summary>
    public class RouteService : IRouteService
    {
        private readonly IMapper _mapper;
        private readonly FlipTripDbContext _flipTripDbContext;

        public RouteService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _flipTripDbContext = flipTripDbContext;
        }

        public async Task<ResultRouteDto> CreateAsync(CreateRouteDto createRouteDto)
        {
            await ValidateTripExistsAsync(createRouteDto.TripId);

            var routeEntity = _mapper.Map<RouteEntity>(createRouteDto);

            routeEntity.DateCreated = DateTimeOffset.Now;

            var entityEntry = _flipTripDbContext.Routes.Add(routeEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var resultDto = _mapper.Map<ResultRouteDto>(entityEntry.Entity);

            return resultDto;
        }

        public async Task<ResultRouteDto> UpdateAsync(UpdateRouteDto updateRouteDto)
        {
            var tripEntity = await _flipTripDbContext
                .Trips
                .Where(tripEntity => tripEntity.Id == updateRouteDto.TripId)
                .Include(tripEntity => tripEntity.Routes)
                .FirstOrDefaultAsync();

            ValidateTripEntityIsNotNull(tripEntity);

            var routeEntity = tripEntity
                .Routes
                .Where(routeEntity => routeEntity.Id == updateRouteDto.Id)
                .FirstOrDefault();

            ValidateRouteEntityIsNotNull(routeEntity);

            routeEntity.Title = updateRouteDto.Title;
            routeEntity.TripId = updateRouteDto.TripId;

            await _flipTripDbContext.SaveChangesAsync();

            var resultRouteDto = _mapper.Map<ResultRouteDto>(routeEntity);

            return resultRouteDto;
        }

        public async Task DeleteAsync(int id)
        {
            var routeEntityToDelete = await _flipTripDbContext
                .Routes
                .SingleOrDefaultAsync(routeEntity => routeEntity.Id == id);

            ValidateRouteEntityIsNotNull(routeEntityToDelete);

            _flipTripDbContext.Routes.Remove(routeEntityToDelete);
            await _flipTripDbContext.SaveChangesAsync();
        }

        public async Task<ResultRouteDto> GetByIdAsync(int routeId)
        {
            var routeEntity = await _flipTripDbContext
                .Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(routeEntity => routeEntity.Id == routeId);

            ValidateRouteEntityIsNotNull(routeEntity);

            var resultRouteDto = _mapper.Map<ResultRouteDto>(routeEntity);

            return resultRouteDto;
        }

        public async Task<IEnumerable<ResultRouteDto>> GetAllByTripIdAsync(int tripId)
        {
            var tripEntity = await _flipTripDbContext
                .Trips
                .AsNoTracking()
                .Where(tripEntity => tripEntity.Id == tripId)
                .Include(tripEntity => tripEntity.Routes)
                .FirstOrDefaultAsync();

            ValidateTripEntityIsNotNull(tripEntity);

            var resultRouteDtoList = _mapper.Map<List<ResultRouteDto>>(tripEntity.Routes.ToList());

            return resultRouteDtoList;
        }

        public async Task<PagedList<ResultRouteDto>> GetPageByTripIdAsync(int tripId, BasicPaginationFilter paginationFilter)
        {
            var tripEntity = await _flipTripDbContext
                .Trips
                .AsNoTracking()
                .Where(tripEntity => tripEntity.Id == tripId)
                .Include(tripEntity => tripEntity.Routes)
                .FirstOrDefaultAsync();

            ValidateTripEntityIsNotNull(tripEntity);

            var resultRouteEntitiesPagedList = tripEntity
                .Routes
                .ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);

            var resultRouteDtoPagedList = _mapper.Map<PagedList<ResultRouteDto>>(resultRouteEntitiesPagedList);

            return resultRouteDtoPagedList;
        }

        /// <summary>
        /// Checks if the given <see cref="TripEntity"/> is not null. If null, then throws an <see cref="ArgumentException"/> with corresponding message.
        /// </summary>
        /// <param name="tripEntity">Object that should be checked.</param>
        void ValidateTripEntityIsNotNull(TripEntity tripEntity)
        {
            if (tripEntity == null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }
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
        /// Checks if Trip exists by making a database query.
        /// </summary>
        async Task ValidateTripExistsAsync(int tripId)
        {
            var tripEntity = await _flipTripDbContext
                .Trips
                .AsNoTracking()
                .SingleOrDefaultAsync(tripEntity => tripId == tripEntity.Id);

            if (tripEntity == null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }
        }
    }
}
