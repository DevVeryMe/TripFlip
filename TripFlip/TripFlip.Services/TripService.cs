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

            if (tripEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

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

        public async Task<TripDto> UpdateAsync(TripDto tripDto)
        {
            var tripEntity = await _tripFlipDbContext.Trips.FindAsync(tripDto.Id);

            if (tripEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            tripEntity.Description = tripDto.Description;
            tripEntity.Title = tripDto.Title;
            tripEntity.StartsAt = tripDto.StartsAt;
            tripEntity.EndsAt = tripDto.EndsAt;

            await _tripFlipDbContext.SaveChangesAsync();
            var newTripDto = _mapper.Map<TripDto>(tripEntity);

            return newTripDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var tripEntity = await _tripFlipDbContext.Trips.FindAsync(id);

            if (tripEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }

            _tripFlipDbContext.Remove(tripEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }
    }
}
