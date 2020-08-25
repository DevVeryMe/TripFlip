using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;
using TripFlip.Services.Interfaces.TripInterfaces;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class TripService : ITripService
    {
        private readonly TripFlipDbContext _tripFlipDbContext;

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="tripFlipDbContext">database context</param>
        /// <param name="mapper">mapper</param>
        public TripService(TripFlipDbContext tripFlipDbContext, IMapper mapper)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
        }

        public async Task<PagedList<TripDto>> GetAllTripsAsync(
            string searchString,
            PaginationDto paginationDto)
        {
            int pageNumber = paginationDto.PageNumber ?? 1;
            int pageSize = paginationDto.PageSize ?? await _tripFlipDbContext.Trips.CountAsync();

            var tripsQuery = _tripFlipDbContext
                .Trips
                .AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                tripsQuery = tripsQuery
                    .Where(tripEntity => tripEntity.Title.Contains(searchString) || 
                        tripEntity.Description.Contains(searchString));
            }

            var tripEntitiesPagedList = tripsQuery.ToPagedList(pageNumber, pageSize);
            var tripDtosPagedList = _mapper.Map<PagedList<TripDto>>(tripEntitiesPagedList);

            return tripDtosPagedList;
        }

        public async Task<TripDto> GetByIdAsync(int id)
        {
            var trip = await _tripFlipDbContext.Trips.AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);

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

            await _tripFlipDbContext.AddAsync(trip);
            await _tripFlipDbContext.SaveChangesAsync();
            var tripDto = _mapper.Map<TripDto>(trip);

            return tripDto;
        }

        public async Task<TripDto> UpdateAsync(TripDto tripDto)
        {
            var tripToUpdate = await _tripFlipDbContext.Trips.FindAsync(tripDto.Id);

            if (tripToUpdate is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            tripToUpdate.Description = tripDto.Description;
            tripToUpdate.Title = tripDto.Title;
            tripToUpdate.StartsAt = tripDto.StartsAt;
            tripToUpdate.EndsAt = tripDto.EndsAt;

            await _tripFlipDbContext.SaveChangesAsync();
            var newTripDto = _mapper.Map<TripDto>(tripToUpdate);

            return newTripDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var tripToDelete = await _tripFlipDbContext.Trips.FindAsync(id);

            if (tripToDelete is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            _tripFlipDbContext.Remove(tripToDelete);
            await _tripFlipDbContext.SaveChangesAsync();
        }
    }
}
