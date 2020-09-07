using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
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

        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor instance.</param>
        public TripService(TripFlipDbContext tripFlipDbContext,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
            var currentUserId = HttpContextClaimsParser.GetUserIdFromClaims(_httpContextAccessor);

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
    }
}
