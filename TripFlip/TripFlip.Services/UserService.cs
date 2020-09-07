﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
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

        private readonly JsonWebTokenConfig _jsonWebTokenConfig;

        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="jsonWebTokenConfig">JsonWebTokenConfig instance.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor instance.</param>
        public UserService(IMapper mapper,
            TripFlipDbContext tripFlipDbContext,
            JsonWebTokenConfig jsonWebTokenConfig, 
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
            _jsonWebTokenConfig = jsonWebTokenConfig;
            _httpContextAccessor = httpContextAccessor;
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
            if (grantSubscriberRoleDto.TripRoleId == (int) TripRoles.Admin)
            {
                throw new ArgumentException(ErrorConstants.NoGrantAdminRolePermission);
            }

            var currentUserId = GetUserIdFromClaims();

            var trip = await _tripFlipDbContext.Trips
                .Include(t => t.TripSubscribers)
                .ThenInclude(subscribers => subscribers.TripRoles)
                .FirstOrDefaultAsync(t => t.Id == grantSubscriberRoleDto.TripId);

            ValidateEntityNotNull<TripEntity>(trip, ErrorConstants.TripNotFound);

            var subscriberEntity = trip.TripSubscribers
                .FirstOrDefault(subscribers => subscribers.UserId == grantSubscriberRoleDto.UserId);

            ValidateEntityNotNull<TripSubscriberEntity>(subscriberEntity, 
                ErrorConstants.NotSubscriberOfTheTrip);

            var currentUserTripAdmin = trip.TripSubscribers
                .FirstOrDefault(subscriber => subscriber.UserId == currentUserId)
                ?.TripRoles
                .FirstOrDefault(role => role.TripRoleId == (int) TripRoles.Admin);

            ValidateEntityNotNull<TripSubscriberRoleEntity>(currentUserTripAdmin, 
                ErrorConstants.NoGrantRolePermission);

            var isRoleAlreadySet = subscriberEntity.TripRoles
                .Any(tripSubscriberRoleEntity => 
                    tripSubscriberRoleEntity.TripRoleId == grantSubscriberRoleDto.TripRoleId);

            if (isRoleAlreadySet)
            {
                throw new ArgumentException(ErrorConstants.AlreadyRoleSet);
            }

            var tripSubscriberRoleEntityToAdd = new TripSubscriberRoleEntity()
            {
                TripSubscriber = subscriberEntity,
                TripRoleId = grantSubscriberRoleDto.TripRoleId
            };

            await _tripFlipDbContext.TripSubscribersRoles.AddAsync(tripSubscriberRoleEntityToAdd);
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
        /// Validates whether entity is not null. If null,
        /// throws an ArgumentException.
        /// </summary>
        /// <typeparam name="TEntity">Any entity to check.</typeparam>
        /// <param name="entity">Instance of TEntity.</param>
        /// <param name="errorMessage">Error message to display.</param>
        private void ValidateEntityNotNull<TEntity>(TEntity entity, string errorMessage)
        {
            if (entity is null)
            {
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// Gets users id from http request user claims.
        /// </summary>
        /// <returns>The users id.</returns>
        private Guid GetUserIdFromClaims()
        {
            var currentUserIdToParse = _httpContextAccessor
                .HttpContext
                .User
                ?.Claims
                ?.SingleOrDefault(c =>
                    c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                ?.Value;

            if (currentUserIdToParse is null)
            {
                throw new UnauthorizedAccessException();
            }

            var currentUserId = Guid.Parse(currentUserIdToParse);

            return currentUserId;
        }

        /// <summary>
        /// Generates JWT.
        /// </summary>
        /// <param name="user">User entity needed to add claims.</param>
        /// <returns>Encoded JWT.</returns>
        private string GenerateJsonWebToken(UserEntity user)
        {
            var encodedSecretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_jsonWebTokenConfig.SecretKey));
                var credentials = new SigningCredentials(
                encodedSecretKey,
                SecurityAlgorithms.HmacSha256
                );

            int expirationTime = _jsonWebTokenConfig.TokenLifetime;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            // creating JWT
            var jwt = new JwtSecurityToken(
                issuer: _jsonWebTokenConfig.Issuer,
                audience: _jsonWebTokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: credentials
                );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
