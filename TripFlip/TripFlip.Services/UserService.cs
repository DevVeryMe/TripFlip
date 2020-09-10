using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Configurations;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Enums;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        private readonly TripFlipDbContext _tripFlipDbContext;

        private readonly JwtConfiguration _jwtConfiguration;

        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="jwtConfiguration">JwtConfiguration instance.</param>
        /// <param name="currentUserService">ICurrentUserService instance.</param>
        public UserService(IMapper mapper,
            TripFlipDbContext tripFlipDbContext,
            JwtConfiguration jwtConfiguration,
            ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
            _jwtConfiguration = jwtConfiguration;
            _currentUserService = currentUserService;
        }

        public async Task<PagedList<UserDto>> GetAllAsync(
            string searchString,
            PaginationDto paginationDto)
        {
            int pageNumber = paginationDto.PageNumber ?? 1;
            int pageSize = paginationDto.PageSize ?? 
                await _tripFlipDbContext.Users.CountAsync();

            var usersQuery = _tripFlipDbContext
                .Users
                .AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                usersQuery = usersQuery
                    .Where(userEntity => userEntity.Email.Contains(searchString));
            }

            var pagedUserEntities = usersQuery.ToPagedList(pageNumber, pageSize);
            var pagedUserDtos = _mapper.Map<PagedList<UserDto>>(pagedUserEntities);

            return pagedUserDtos;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var userEntity = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Id == id);

            ValidateUserEntityNotNull(userEntity);

            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public async Task<AuthenticatedUserDto> AuthorizeAsync(LoginDto loginDto)
        {
            var userEntity = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .Include(user => user.ApplicationRoles)
                .ThenInclude(usersRoles => usersRoles.ApplicationRole)
                .FirstOrDefaultAsync(user => user.Email == loginDto.Email);

            ValidateUserEntityNotNull(userEntity);

            bool isPasswordVerified = PasswordHasherHelper
                .VerifyPassword(loginDto.Password, userEntity.PasswordHash);

            if (!isPasswordVerified)
            {
                throw new ArgumentException(ErrorConstants.PasswordNotVerified);
            }

            AuthenticatedUserDto authenticatedUserDto = 
                _mapper.Map<AuthenticatedUserDto>(userEntity);

            authenticatedUserDto.Token = GenerateJsonWebToken(userEntity);

            return authenticatedUserDto;
        }

        public async Task<UserDto> RegisterAsync(RegisterUserDto registerUserDto)
        {
            bool emailIsAlreadyTaken = _tripFlipDbContext
                .Users
                .Any(user => user.Email == registerUserDto.Email);

            if (emailIsAlreadyTaken)
            {
                throw new ArgumentException(ErrorConstants.EmailIsTaken);
            }

            var userEntity = _mapper.Map<UserEntity>(registerUserDto);

            userEntity.PasswordHash = 
                PasswordHasherHelper.HashPassword(registerUserDto.Password);

            _tripFlipDbContext.Users.Add(userEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public async Task<UserDto> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var userEntity = await _tripFlipDbContext.Users.FindAsync(updateUserDto.Id);

            ValidateUserEntityNotNull(userEntity);

            userEntity.Email = updateUserDto.Email;

            await _tripFlipDbContext.SaveChangesAsync();
            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var userEntity = await _tripFlipDbContext.Users.FindAsync(id);

            ValidateUserEntityNotNull(userEntity);

            _tripFlipDbContext.Remove(userEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task GrantRoleAsync(GrantSubscriberRoleDto grantSubscriberRoleDto)
        {
            var currentUserIdString = _currentUserService.UserId;
            var currentUserId = Guid.Parse(currentUserIdString);

            var userToGrantRoleExists = await _tripFlipDbContext.Users
                .AnyAsync(user => user.Id == grantSubscriberRoleDto.UserId);

            if (!userToGrantRoleExists)
            {
                throw new ArgumentException(ErrorConstants.UserNotFound);
            }

            var trip = await _tripFlipDbContext.Trips
                .Include(t => t.TripSubscribers)
                .ThenInclude(subscribers => subscribers.TripRoles)
                .FirstOrDefaultAsync(t => t.Id == grantSubscriberRoleDto.TripId);

            EntityValidationHelper.
                ValidateEntityNotNull<TripEntity>(trip, ErrorConstants.TripNotFound);

            var currentUserTripAdmin = trip.TripSubscribers
                .FirstOrDefault(subscriber => subscriber.UserId == currentUserId)
                ?.TripRoles
                .FirstOrDefault(role => role.TripRoleId == (int) TripRoles.Admin);
            
            EntityValidationHelper.
                ValidateEntityNotNull<TripSubscriberRoleEntity>(currentUserTripAdmin, 
                ErrorConstants.NoGrantRolePermission);

            var userSubscriber = trip.TripSubscribers
                .FirstOrDefault(subscribers => subscribers.UserId == grantSubscriberRoleDto.UserId);

            // If user is already subscribed, checking it's roles, otherwise subscribes.
            if (userSubscriber != null)
            {
                var sameUserRole = userSubscriber.TripRoles
                    .FirstOrDefault(tripSubscriberRoleEntity =>
                        tripSubscriberRoleEntity.TripRoleId == grantSubscriberRoleDto.TripRoleId);

                if (sameUserRole != null)
                {
                    throw new ArgumentException(ErrorConstants.AlreadyRoleSet);
                }
            }
            else
            {
                userSubscriber = new TripSubscriberEntity()
                {
                    TripId = grantSubscriberRoleDto.TripId,
                    UserId = grantSubscriberRoleDto.UserId
                };
            }

            var tripSubscriberRoleEntityToAdd = new TripSubscriberRoleEntity()
            {
                TripSubscriber = userSubscriber,
                TripRoleId = grantSubscriberRoleDto.TripRoleId
            };

            await _tripFlipDbContext.TripSubscribersRoles.AddAsync(tripSubscriberRoleEntityToAdd);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task SubscribeToTripAsync(int tripId)
        {
            var currentUserIdString = _currentUserService.UserId;
            var currentUserId = Guid.Parse(currentUserIdString);

            var userExists = await _tripFlipDbContext.Users
                .AnyAsync(user => user.Id == currentUserId);

            if (!userExists)
            {
                throw new ArgumentException(ErrorConstants.NotAuthorized);
            }

            var tripEntity = await _tripFlipDbContext.Trips
                .Include(t => t.TripSubscribers)
                .FirstOrDefaultAsync(t => t.Id == tripId);

            EntityValidationHelper.ValidateEntityNotNull(tripEntity, ErrorConstants.TripNotFound);

            var isAlreadySubscriber = tripEntity.TripSubscribers
                .Any(subscriber => subscriber.UserId == currentUserId);

            if (isAlreadySubscriber)
            {
                throw new ArgumentException(ErrorConstants.IsAlreadySubscriber);
            }

            var subscriberRole = new TripSubscriberRoleEntity()
            {
                TripSubscriber = new TripSubscriberEntity()
                {
                    UserId = currentUserId,
                    TripId = tripId
                },

                TripRoleId = (int)TripRoles.Guest
            };

            await _tripFlipDbContext.TripSubscribersRoles.AddAsync(subscriberRole);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the given <see cref="UserEntity"/> is not null. If null,
        /// then throws an <see cref="ArgumentException"/> with a corresponding message.
        /// </summary>
        /// <param name="userEntity">Object that should be checked.</param>
        private void ValidateUserEntityNotNull(UserEntity userEntity)
        {
            if (userEntity is null)
            {
                throw new ArgumentException(ErrorConstants.UserNotFound);
            }
        }

        /// <summary>
        /// Generates JWT.
        /// </summary>
        /// <param name="userIncludingRoles">User entity with included roles.</param>
        /// <returns>Encoded JWT.</returns>
        private string GenerateJsonWebToken(UserEntity userIncludingRoles)
        {
            var encodedSecretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_jwtConfiguration.SecretKey));
                var credentials = new SigningCredentials(
                encodedSecretKey,
                SecurityAlgorithms.HmacSha256
                );

            int expirationTime = _jwtConfiguration.TokenLifetime;

            var roles = userIncludingRoles.ApplicationRoles
                .Select(role => new {Id = role.ApplicationRole.Id, Name = role.ApplicationRole.Name});

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userIncludingRoles.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userIncludingRoles.Email),
                new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(roles))
            };

            // creating JWT
            var jwt = new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: credentials
                );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
