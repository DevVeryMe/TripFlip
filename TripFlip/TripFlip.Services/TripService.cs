using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TripFlip.DataAccess;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces.TripInterfaces;

namespace TripFlip.Services
{
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
            var trip = await _flipTripDbContext.Trips.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            if (trip is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            var tripDto = _mapper.Map<TripDto>(trip);

            return tripDto;
        }

        public void CreateTrip(TripDto tripDto)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTrip(TripDto tripDto)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTrip(TripDto tripDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
