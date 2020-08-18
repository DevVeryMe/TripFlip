using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using TripFlip.Services.Interfaces;
using TripFlip.Services.DTO;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;

namespace TripFlip.Services
{
    /// <summary>
    /// Class that performs CRUD operations related to <see cref="RouteEntity"/>
    /// </summary>
    public class RouteService : IRouteService
    {
        IMapper _mapper;
        FlipTripDbContext _flipTripDbContext;

        public RouteService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _flipTripDbContext = flipTripDbContext;
        }

        public async Task<RouteDto> CreateAsync(RouteDto routeDto)
        {
            bool tripExists = await TripExistsAsync(routeDto.TripId);
            if (!tripExists)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            routeDto.DateCreated = DateTimeOffset.Now;

            RouteEntity routeEntity = _mapper.Map<RouteEntity>(routeDto);

            var entityEntry = _flipTripDbContext.Routes.Add(routeEntity);
            await _flipTripDbContext.SaveChangesAsync();

            routeDto.Id = entityEntry.Entity.Id;

            return routeDto;
        }

        public async Task<RouteDto> UpdateAsync(RouteDto routeDto)
        {
            bool routeExists = await RouteExistsAsync(routeDto.Id);
            if (!routeExists)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }

            bool tripExists = await TripExistsAsync(routeDto.TripId);
            if (!tripExists)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            // set original creation date
            routeDto.DateCreated = (await _flipTripDbContext
                    .Routes
                    .AsNoTracking()
                    .FirstAsync(routeEntity => routeEntity.Id == routeDto.Id)
                )
                .DateCreated;

            var routeEntity = _mapper.Map<RouteEntity>(routeDto);

            var entityEntry = _flipTripDbContext.Routes.Update(routeEntity);
            await _flipTripDbContext.SaveChangesAsync();

            routeDto.DateCreated = entityEntry.Entity.DateCreated;

            return routeDto;
        }

        public async Task<RouteDto> GetAsync(int routeId)
        {
            var routeEntity = await _flipTripDbContext
                .Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(routeEntity => routeEntity.Id == routeId);

            if (routeEntity == null)
            {
                throw new ArgumentException(ErrorConstants.RouteNotFound);
            }

            var routeDto = _mapper.Map<RouteDto>(routeEntity);

            return routeDto;
        }

        /// <summary>
        /// Checks if Trip exists by making a database query. Returns true if Trip with the given Id exists. Otherwise returns false.
        /// </summary>
        async Task<bool> TripExistsAsync(int tripId)
        {
            var tripEntity = await _flipTripDbContext.Trips
                .AsNoTracking()
                .SingleOrDefaultAsync(tripEntity => tripId == tripEntity.Id);

            return tripEntity != null;
        }

        /// <summary>
        /// Checks if Route exists by making a database query. Returns true if Route with the given Id exists. Otherwise returns false.
        /// </summary>
        async Task<bool> RouteExistsAsync(int routeId)
        {
            var routeEntity = await _flipTripDbContext.Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(routeEntity => routeId == routeEntity.Id);

            return routeEntity != null;
        }
    }
}
