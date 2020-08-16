using System.Threading.Tasks;
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
            RouteEntity routeEntity = EntityFromDto(routeDto);

            var entityEntry = _flipTripDbContext.Routes.Add(routeEntity);
            await _flipTripDbContext.SaveChangesAsync();

            routeDto.Id = entityEntry.Entity.Id;

            return routeDto;
        }

        RouteEntity EntityFromDto(RouteDto routeDto)
        {
            return _mapper.Map<RouteEntity>(routeDto);
        }
    }
}
