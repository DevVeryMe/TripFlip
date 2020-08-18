using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TripDtos;
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

        public async Task<TripDto> CreateAsync(CreateTripDto createTripDto)
        {
            var trip = _mapper.Map<TripEntity>(createTripDto);
            trip.DateCreated = DateTimeOffset.Now;

            await _flipTripDbContext.AddAsync(trip);
            await _flipTripDbContext.SaveChangesAsync();
            var tripDto = _mapper.Map<TripDto>(trip);

            return tripDto;
        }

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

        public async Task DeleteAsync(int id)
        {
            var tripToDelete = await _flipTripDbContext.Trips.FindAsync(id);

            if (tripToDelete is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            _flipTripDbContext.Remove(tripToDelete);
            await _flipTripDbContext.SaveChangesAsync();
        }
    }
}
