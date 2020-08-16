using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TripDtos;
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

        /// <inheritdoc />
        public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
        {
            var trips = await _flipTripDbContext.Trips.AsNoTracking().ToListAsync();
            var tripDtos = _mapper.Map<List<TripDto>>(trips);

            return tripDtos;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<TripDto> CreateAsync(TripDto tripDto)
        {
            var mappedTrip = _mapper.Map<TripEntity>(tripDto);
            mappedTrip.DateCreated = DateTimeOffset.Now;

            var newTrip = _flipTripDbContext.Add(mappedTrip);
            await _flipTripDbContext.SaveChangesAsync();

            return tripDto;
        }

        /// <inheritdoc />
        public void UpdateTrip(TripDto tripDto)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void DeleteTrip(TripDto tripDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
