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
using TripFlip.Services.Interfaces;

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

            var pagedTripEntities = tripsQuery.ToPagedList(pageNumber, pageSize);
            var pagedTripDtos = _mapper.Map<PagedList<TripDto>>(pagedTripEntities);

            return pagedTripDtos;
        }

        public async Task<TripDto> GetByIdAsync(int id)
        {
            var tripEntity = await _tripFlipDbContext.Trips
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            ValidateTripEntityNotNull(tripEntity);

            var tripDto = _mapper.Map<TripDto>(tripEntity);

            return tripDto;
        }

        public async Task<TripDto> CreateAsync(CreateTripDto createTripDto)
        {
            var tripEntity = _mapper.Map<TripEntity>(createTripDto);

            await _tripFlipDbContext.AddAsync(tripEntity);
            await _tripFlipDbContext.SaveChangesAsync();
            var tripDto = _mapper.Map<TripDto>(tripEntity);

            return tripDto;
        }

        public async Task<TripDto> UpdateAsync(UpdateTripDto updateTripDto)
        {
            var tripEntity = await _tripFlipDbContext.Trips.FindAsync(updateTripDto.Id);

            ValidateTripEntityNotNull(tripEntity);

            tripEntity.Description = updateTripDto.Description;
            tripEntity.Title = updateTripDto.Title;
            tripEntity.StartsAt = updateTripDto.StartsAt;
            tripEntity.EndsAt = updateTripDto.EndsAt;

            await _tripFlipDbContext.SaveChangesAsync();
            var tripDto = _mapper.Map<TripDto>(tripEntity);

            return tripDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var tripEntity = await _tripFlipDbContext.Trips.FindAsync(id);

            ValidateTripEntityNotNull(tripEntity);

            _tripFlipDbContext.Remove(tripEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        void ValidateTripEntityNotNull(TripEntity tripEntity)
        {
            if (tripEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }
        }
    }
}
