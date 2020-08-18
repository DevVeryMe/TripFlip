using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            // check by the given TripId if trip exists
            var tripEntityCount = await _flipTripDbContext.Trips
                .AsNoTracking()
                .CountAsync(tripEntity => routeDto.TripId == tripEntity.Id);
            if (tripEntityCount == 0)
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
    }
}
