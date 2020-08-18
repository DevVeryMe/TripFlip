using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces.TripInterfaces;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class TripService : ITripService
    {
        private readonly FlipTripDbContext _flipTripDbContext;

        private readonly IMapper _mapper;

        public TripService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _flipTripDbContext = flipTripDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
        {
            var trips = await _flipTripDbContext.Trips.AsNoTracking().ToListAsync();
            var tripDtos = _mapper.Map<List<TripDto>>(trips);

            return tripDtos;
        }
        public async Task<TripDto> GetAsync(int id)
        {
            var trip = await _flipTripDbContext.Trips.AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);

            if (trip is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            var tripDto = _mapper.Map<TripDto>(trip);

            return tripDto;
        }
    }
}
