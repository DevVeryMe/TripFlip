using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Enums;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class TripService : ITripService
    {
        private readonly TripFlipDbContext _tripFlipDbContext;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="currentUserService">ICurrentUserService instance.</param>
        public TripService(TripFlipDbContext tripFlipDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
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

            EntityValidationHelper
                .ValidateEntityNotNull(tripEntity, ErrorConstants.TripNotFound);

            var tripDto = _mapper.Map<TripDto>(tripEntity);

            return tripDto;
        }

        public async Task<TripDto> CreateAsync(CreateTripDto createTripDto)
        {
            var tripEntity = _mapper.Map<TripEntity>(createTripDto);
            var currentUserId = _currentUserService.UserId;

            await ValidateUserExistsById(currentUserId);
            await ValidateTripRoleExistsById((int)TripRoles.Admin);

            var tripSubscriberEntity = new TripSubscriberEntity()
            {
                UserId = currentUserId,
                Trip = tripEntity
            };

            var tripSubscriberRoleEntity = new TripSubscriberRoleEntity()
            {
                TripSubscriber = tripSubscriberEntity,
                TripRoleId = (int)TripRoles.Admin
            };

            await _tripFlipDbContext.TripSubscribersRoles.AddAsync(tripSubscriberRoleEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var tripDto = _mapper.Map<TripDto>(tripEntity);

            return tripDto;
        }

        public async Task<TripDto> UpdateAsync(UpdateTripDto updateTripDto)
        {
            await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
                _currentUserService,
                _tripFlipDbContext,
                updateTripDto.Id,
                TripRoles.Admin,
                ErrorConstants.NotTripAdmin);

            var tripEntity = await _tripFlipDbContext.Trips.FindAsync(updateTripDto.Id);

            EntityValidationHelper
                .ValidateEntityNotNull(tripEntity, ErrorConstants.TripNotFound);

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
            await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
                _currentUserService,
                _tripFlipDbContext,
                id,
                TripRoles.Admin,
                ErrorConstants.NotTripAdmin);

            var tripEntity = await _tripFlipDbContext.Trips
                .Include(trip => trip.Routes)
                .FirstOrDefaultAsync(trip => trip.Id == id);

            EntityValidationHelper
                .ValidateEntityNotNull(tripEntity, ErrorConstants.TripNotFound);

            _tripFlipDbContext.RemoveRange(tripEntity.Routes);
            _tripFlipDbContext.Remove(tripEntity);
            
            await _tripFlipDbContext.SaveChangesAsync();
        }

        private async Task ValidateUserExistsById(Guid userId)
        {
            bool userExists = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .AnyAsync(u => u.Id == userId);

            if (!userExists)
            {
                throw new NotFoundException(ErrorConstants.UserNotFound);
            }
        }

        private async Task ValidateTripRoleExistsById(int tripRoleId)
        {
            bool tripRoleExists = await _tripFlipDbContext
                .TripRoles
                .AsNoTracking()
                .AnyAsync(r => r.Id == tripRoleId);

            if (!tripRoleExists)
            {
                throw new NotFoundException(ErrorConstants.TripRoleNotFound);
            }
        }
    }
}
