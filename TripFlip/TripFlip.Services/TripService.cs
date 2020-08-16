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
        public async Task<TripDto> CreateAsync(CreateTripDto createTripDto)
        {
            var trip = _mapper.Map<TripEntity>(createTripDto);
            trip.DateCreated = DateTimeOffset.Now;

            _flipTripDbContext.Add(trip);
            await _flipTripDbContext.SaveChangesAsync();
            var tripDto = _mapper.Map<TripDto>(trip);

            return tripDto;
        }

        /// <inheritdoc />
        public async Task<TripDto> UpdateAsync(TripDto tripDto)
        {
            var tripToUpdate = await _flipTripDbContext.Trips.FindAsync(tripDto.Id);

            if (tripToUpdate is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            tripToUpdate.Description = tripDto.Description;
            tripToUpdate.Title = tripDto.Title;
            tripToUpdate.StartsAt = tripDto.StartsAt;
            tripToUpdate.EndsAt = tripDto.EndsAt;

            await _flipTripDbContext.SaveChangesAsync();
            var newTripDto = _mapper.Map<TripDto>(tripToUpdate);

            return newTripDto;
        }

        /// <inheritdoc />
        public void DeleteTrip(TripDto tripDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
