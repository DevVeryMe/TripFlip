using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TripFlip.Services.Interfaces;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;

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
            await ValidateRouteExistsAsync(updateRouteDto.Id);
            await ValidateTripExistsAsync(updateRouteDto.TripId);

            var routeEntity = await _flipTripDbContext
               .Routes
               .SingleOrDefaultAsync(routeEntity => routeEntity.Id == updateRouteDto.Id);

            routeEntity.Title = updateRouteDto.Title;
            routeEntity.TripId = updateRouteDto.TripId;

            _flipTripDbContext.Routes.Update(routeEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var resultRouteDto = _mapper.Map<ResultRouteDto>(routeEntity);

            return resultRouteDto;
        }

        public async Task<ResultRouteDto> GetByIdAsync(int routeId)
        {
            var routeEntity = await _flipTripDbContext
                .Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(routeEntity => routeEntity.Id == routeId);

            if (routeEntity == null)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }

            var resultRouteDto = _mapper.Map<ResultRouteDto>(routeEntity);

            return resultRouteDto;
        }

        public async Task<IEnumerable<ResultRouteDto>> GetAllByTripIdAsync(int tripId)
        {
            await ValidateTripExistsAsync(tripId);

            var routeEntityList = await _flipTripDbContext
                .Routes
                .AsNoTracking()
                .Where(routeEntity => routeEntity.TripId == tripId)
                .ToListAsync();

            var resultRouteDtoList = _mapper.Map<List<ResultRouteDto>>(routeEntityList);

            return resultRouteDtoList;
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

        /// <summary>
        /// Checks if Route exists by making a database query.
        /// </summary>
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
