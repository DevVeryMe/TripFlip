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

            ValidateTripEntityNotNull(tripEntity);

            var tripDto = _mapper.Map<TripDto>(tripEntity);

            return tripDto;
        }

        public async Task<TripDto> CreateAsync(CreateTripDto createTripDto)
        {
            var tripEntity = _mapper.Map<TripEntity>(createTripDto);
            var currentUserIdString = _currentUserService.UserId;
            var currentUserId = Guid.Parse(currentUserIdString);

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
            await ValidateCurrentUserIsTripAdminAsync(updateTripDto.Id);

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
            await ValidateCurrentUserIsTripAdminAsync(id);

            var tripEntity = await _tripFlipDbContext.Trips.FindAsync(id);

            ValidateTripEntityNotNull(tripEntity);

            _tripFlipDbContext.Remove(tripEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        private void ValidateTripEntityNotNull(TripEntity tripEntity)
        {
            if (tripEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TripNotFound);
            }
        }

        private async Task ValidateUserExistsById(Guid userId)
        {
            bool userExists = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .AnyAsync(u => u.Id == userId);

            if (!userExists)
            {
                throw new ArgumentException(ErrorConstants.UserNotFound);
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
                throw new ArgumentException(ErrorConstants.TripRoleNotFound);
            }
        }

        /// <summary>
        /// Validates whether current user is trip admin.
        /// </summary>
        /// <param name="tripId">Trip id.</param>
        private async Task ValidateCurrentUserIsTripAdminAsync(int tripId)
        {
            var currentUserIdString = _currentUserService.UserId;
            var currentUserId = Guid.Parse(currentUserIdString);

            var tripSubscriberEntity = await _tripFlipDbContext
                .TripSubscribers
                .AsNoTracking()
                .Include(tripSubscriber => tripSubscriber.TripRoles)
                .SingleOrDefaultAsync(tripSubscriber =>
                tripSubscriber.UserId == currentUserId
                && tripSubscriber.TripId == tripId);

            EntityValidationHelper
                .ValidateEntityNotNull(tripSubscriberEntity,
                ErrorConstants.TripSubscriberNotFound);

            var tripSubscriberIsAdmin = tripSubscriberEntity
                .TripRoles
                .Any(tripRole =>
                tripRole.TripRoleId == (int)TripRoles.Admin);

            if (!tripSubscriberIsAdmin)
            {
                throw new AccessDeniedException(ErrorConstants.NotTripAdmin);
            }
        }
    }
}
