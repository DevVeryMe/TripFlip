using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<TripDto> GetAllTrips()
        {
            var trips = _flipTripDbContext.Trips.AsNoTracking().ToList();
            var tripDtos = _mapper.Map<List<TripDto>>(trips);

            return tripDtos;
        }

        public TripDto GetTrip(int id)
        {
            throw new System.NotImplementedException();
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
