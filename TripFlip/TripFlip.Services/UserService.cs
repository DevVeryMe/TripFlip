﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Configurations;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.Enums;
using TripFlip.Services.Dto.TripDtos;
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

        private readonly IWebHostEnvironment _environment;

        private readonly IMailService _mailService;

        private readonly MailServiceConfiguration _mailServiceConfiguration;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="jwtConfiguration">JwtConfiguration instance.</param>
        /// <param name="currentUserService">ICurrentUserService instance.</param>
        /// <param name="environment">Instance of web host environment.</param>
        /// <param name="mailService">Instance of mail service.</param>
        /// <param name="mailServiceConfiguration">Object that encapsulates configurations for
        /// mail service.</param>
        public UserService(IMapper mapper,
            TripFlipDbContext tripFlipDbContext,
            JwtConfiguration jwtConfiguration,
            ICurrentUserService currentUserService,
            IWebHostEnvironment environment,
            IMailService mailService,
            MailServiceConfiguration mailServiceConfiguration)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
            _jwtConfiguration = jwtConfiguration;
            _currentUserService = currentUserService;
            _environment = environment;
            _mailService = mailService;
            _mailServiceConfiguration = mailServiceConfiguration;
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

        public async Task<UsersByTripAndCategorizedByRoleDto> 
            GetAllByTripIdAndCategorizeByRoleAsync(int tripId)
        {
            var tripSubscribersList = await _tripFlipDbContext
                .TripSubscribers
                .AsNoTracking()
                .Include(subscriber => subscriber.User)
                .Include(subscriber => subscriber.TripRoles)
                .Include(subscriber => subscriber.Trip)
                .Where(subscriber => subscriber.TripId == tripId)
                .ToListAsync();

            // Trip exists check.
            EntityValidationHelper.ValidateEntityNotNull(
                tripSubscribersList.FirstOrDefault()?.Trip, ErrorConstants.TripNotFound);

            // Get subscribers lists by each role.
            var tripAdmins = GetSubscribedUsersByRole(tripSubscribersList, 
                (int)TripRoles.Admin);
            var tripEditors = GetSubscribedUsersByRole(tripSubscribersList, 
                (int)TripRoles.Editor);
            var tripGuests = GetSubscribedUsersByRole(tripSubscribersList, 
                (int)TripRoles.Guest);

            // Map entities to DTOs.
            var tripAdminsDtos = _mapper.Map<IEnumerable<UserDto>>(tripAdmins);
            var tripEditorsDtos = _mapper.Map<IEnumerable<UserDto>>(tripEditors);
            var tripGuestsDtos = _mapper.Map<IEnumerable<UserDto>>(tripGuests);

            // Create result DTO.
            return new UsersByTripAndCategorizedByRoleDto()
            {
                TripId = tripId,
                TripAdmins = tripAdminsDtos,
                TripEditors = tripEditorsDtos,
                TripGuests = tripGuestsDtos
            };
        }

        /// <summary>
        /// Makes a LINQ query to a given source collection of trip subscribers 
        /// and returns collection of users with a role that matches a given role id.
        /// </summary>
        /// <param name="source">Source collection of trip subscribers to search in.</param>
        /// <param name="roleId">Role id to search users with.</param>
        /// <returns>Collection of users with a role that matches a given role id.</returns>
        IEnumerable<UserEntity> GetSubscribedUsersByRole(
            IEnumerable<TripSubscriberEntity> source,
            int roleId)
        {
            var subbedUsersByRole = source
                .Where(subscriber => subscriber
                    .TripRoles
                    .Any(tripRole => tripRole.TripRoleId == roleId))
                .Select(subscriber => subscriber.User);

            return subbedUsersByRole;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var userEntity = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Id == id);

            EntityValidationHelper.ValidateEntityNotNull(
                userEntity, ErrorConstants.UserNotFound);

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

            EntityValidationHelper.ValidateEntityNotNull(
                userEntity, ErrorConstants.UserNotFound);

            bool isPasswordVerified = PasswordHasherHelper
                .VerifyPassword(loginDto.Password, userEntity.PasswordHash);

            if (!isPasswordVerified)
            {
                throw new ArgumentException(ErrorConstants.PasswordNotVerified);
            }

            AuthenticatedUserDto authenticatedUserDto = 
                _mapper.Map<AuthenticatedUserDto>(userEntity);

            authenticatedUserDto.Token = JsonWebTokenHelper.GenerateJsonWebToken(
                userIncludingRoles: userEntity,
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                secretKey: _jwtConfiguration.SecretKey,
                tokenLifetime: _jwtConfiguration.TokenLifetime);

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

            await _tripFlipDbContext.Users.AddAsync(userEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            await EmailUserNotifierHelper.NotifyRegisteredUserAsync(
                userEntity.Email, _environment, _mailService, _mailServiceConfiguration);

            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public async Task<UserDto> UpdateUserProfileAsync(UpdateUserProfileDto updateUserDto)
        {
            Guid userId = _currentUserService.UserId;

            var userEntity = await _tripFlipDbContext
                .Users
                .FindAsync(userId);

            EntityValidationHelper.ValidateEntityNotNull(
                userEntity, ErrorConstants.UserNotFound);

            userEntity.Email = updateUserDto.Email;
            userEntity.FirstName = updateUserDto.FirstName;
            userEntity.LastName = updateUserDto.LastName;
            userEntity.AboutMe = updateUserDto.AboutMe;
            userEntity.Gender =  (TripFlip.Domain.Entities.Enums.UserGender?) updateUserDto.Gender;
            userEntity.BirthDate = updateUserDto.BirthDate;

            await _tripFlipDbContext.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public async Task ChangePasswordAsync(ChangeUserPasswordDto changeUserPasswordDto)
        {
            Guid userId = _currentUserService.UserId;

            var userEntity = await _tripFlipDbContext
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);

            EntityValidationHelper.ValidateEntityNotNull(
                userEntity, ErrorConstants.UserNotFound);

            bool passwordIsVerified = PasswordHasherHelper.VerifyPassword(
                changeUserPasswordDto.OldPassword, userEntity.PasswordHash);
            if (!passwordIsVerified)
            {
                throw new ArgumentException(ErrorConstants.PasswordNotVerified);
            }

            string newHashedPassword = PasswordHasherHelper.HashPassword(
                changeUserPasswordDto.NewPassword);

            userEntity.PasswordHash = newHashedPassword;

            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await EntityValidationHelper
                .ValidateCurrentUserIsSuperAdminAsync(_currentUserService, _tripFlipDbContext);

            var userEntity = await _tripFlipDbContext.Users.FindAsync(id);

            EntityValidationHelper.ValidateEntityNotNull(
                userEntity, ErrorConstants.UserNotFound);

            _tripFlipDbContext.Remove(userEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task GrantApplicationRoleAsync(GrantApplicationRolesDto grantApplicationRolesDto)
        {
            // Validate not trying to grant application super admin role.
            bool isGrantingSuperAdminRole = grantApplicationRolesDto
                .ApplicationRoleIds
                .Any(appRoleId => appRoleId == (int)ApplicationRole.SuperAdmin);

            if (isGrantingSuperAdminRole)
            {
                throw new ArgumentException(ErrorConstants.GrantingSuperAdminRole);
            }

            // Validate current user is application super admin.
            await EntityValidationHelper
                .ValidateCurrentUserIsSuperAdminAsync(_currentUserService, _tripFlipDbContext);

            // Validate user-to-grant-roles-to exists.
            var userToGrantRoles = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .Include(user => user.ApplicationRoles)
                .SingleOrDefaultAsync(user => user.Id == grantApplicationRolesDto.UserId);

            EntityValidationHelper
                .ValidateEntityNotNull(userToGrantRoles, ErrorConstants.UserNotFound);

            // Remove user's current set of roles.
            if (!(userToGrantRoles.ApplicationRoles is null))
            {
                _tripFlipDbContext.ApplicationUsersRoles.RemoveRange(
                    userToGrantRoles.ApplicationRoles);
            }

            // Remove invalid values from requested role id collection.
            var existingApplicationRolesIds = (IEnumerable<int>)Enum.GetValues(typeof(ApplicationRole));
            grantApplicationRolesDto.ApplicationRoleIds = grantApplicationRolesDto
                .ApplicationRoleIds
                .Distinct()
                .Where(requestedId => existingApplicationRolesIds.Contains(requestedId))
                .ToList();

            // Add requested set of roles to user.
            bool collectionHasRolesToAdd = grantApplicationRolesDto.ApplicationRoleIds.Any();
            if (collectionHasRolesToAdd)
            {
                var rolesToAdd = new List<ApplicationUserRoleEntity>();

                foreach (var requestedRole in grantApplicationRolesDto.ApplicationRoleIds)
                {
                    rolesToAdd.Add(new ApplicationUserRoleEntity()
                    {
                        UserId = userToGrantRoles.Id,
                        ApplicationRoleId = requestedRole
                    });
                }

                await _tripFlipDbContext.ApplicationUsersRoles.AddRangeAsync(rolesToAdd);
            }
            
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task GrantTripRoleAsync(GrantTripRolesDto grantTripRolesDto)
        {
            // Validate user-to-grant-role-to exists.
            var userToGrantRole = await _tripFlipDbContext.Users
                .SingleOrDefaultAsync(user => user.Id == grantTripRolesDto.UserId);

            EntityValidationHelper.ValidateEntityNotNull(
                userToGrantRole, ErrorConstants.UserNotFound);

            // Validate trip exists.
            var trip = await _tripFlipDbContext.Trips
                .Include(t => t.TripSubscribers)
                .ThenInclude(subscribers => subscribers.TripRoles)
                .FirstOrDefaultAsync(t => t.Id == grantTripRolesDto.TripId);

            EntityValidationHelper.
                ValidateEntityNotNull(trip, ErrorConstants.TripNotFound);

            // Validate current user is trip admin.
            await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
                _currentUserService,
                _tripFlipDbContext,
                grantTripRolesDto.TripId,
                TripRoles.Admin,
                ErrorConstants.NotTripAdmin);

            // Remove invalid values from requested role id collection.
            var realTripRolesIds = (IEnumerable<int>) Enum.GetValues(typeof(TripRoles));
            grantTripRolesDto.TripRoleIds = grantTripRolesDto
                .TripRoleIds
                .Distinct()
                .Where(requestedId => realTripRolesIds.Contains(requestedId));

            var tripSubscriber = trip.TripSubscribers
                .FirstOrDefault(subscribers => subscribers.UserId == grantTripRolesDto.UserId);

            // Subscribe given user to trip if he's not subscribed.
            if (tripSubscriber is null)
            {
                tripSubscriber = new TripSubscriberEntity()
                {
                    TripId = grantTripRolesDto.TripId,
                    UserId = grantTripRolesDto.UserId
                };
            }

            // Remove subscriber's current set of roles.
            if (!(tripSubscriber.TripRoles is null))
            {
                _tripFlipDbContext.TripSubscribersRoles.RemoveRange(
                    tripSubscriber.TripRoles);
            }

            // Add requested set of roles to subscriber.
            tripSubscriber.TripRoles = grantTripRolesDto.TripRoleIds
                .Select(tripRoleId => new TripSubscriberRoleEntity()
                {
                    TripRoleId = tripRoleId,
                    TripSubscriber = tripSubscriber
                }).ToList();

            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task GrantRouteRoleAsync(GrantRouteRolesDto grantRouteRolesDto)
        {
            var routeEntity = await _tripFlipDbContext.Routes
                .Include(route => route.Trip)
                    .ThenInclude(trip => trip.TripSubscribers)
                .Include(route => route.RouteSubscribers)
                    .ThenInclude(routeSubscriber => routeSubscriber.RouteRoles)
                .FirstOrDefaultAsync(route => route.Id == grantRouteRolesDto.RouteId);

            // Validate route exists.
            EntityValidationHelper.ValidateEntityNotNull(routeEntity, ErrorConstants.RouteNotFound);

            // Validate current user is trip admin.
            await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
               _currentUserService,
               _tripFlipDbContext,
               routeEntity.Trip.Id,
               TripRoles.Admin,
               ErrorConstants.NotTripAdmin);

            var tripSubscriberToGrant = routeEntity.Trip.TripSubscribers
                .FirstOrDefault(tripSubscriber => tripSubscriber.UserId == grantRouteRolesDto.UserId);

            // Validate user is the subscriber of a trip, which contains current route.
            EntityValidationHelper.ValidateEntityNotNull(tripSubscriberToGrant, 
                ErrorConstants.TripSubscriberNotFound);

            // Remove invalid values from requested role id collection.
            var realRouteRolesIds = (IEnumerable<int>)Enum.GetValues(typeof(TripRoles));
            grantRouteRolesDto.RouteRoleIds = grantRouteRolesDto
                .RouteRoleIds
                .Distinct()
                .Where(requestedId => realRouteRolesIds.Contains(requestedId));

            var routeSubscriber = routeEntity.RouteSubscribers
                .FirstOrDefault(routeSubscriber =>
                    routeSubscriber.TripSubscriber.UserId == grantRouteRolesDto.UserId);

            // If trip subscriber is not route subscriber - subscribes to route,
            // otherwise removes all his route roles.
            if (routeSubscriber is null)
            {
                routeSubscriber = new RouteSubscriberEntity()
                {
                    TripSubscriberId = tripSubscriberToGrant.Id,
                    RouteId = routeEntity.Id
                };

                await _tripFlipDbContext.RouteSubscribers.AddAsync(routeSubscriber);
            }
            else
            {
                routeSubscriber.RouteRoles.Clear();
            }

            routeSubscriber.RouteRoles = grantRouteRolesDto.RouteRoleIds
                .Select(routeRoleId => new RouteSubscriberRoleEntity()
                {
                    RouteRoleId = routeRoleId,
                    RouteSubscriber = routeSubscriber
                }).ToList();

            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task SubscribeToRouteAsync(int routeId)
        {
            Guid currentUserId = _currentUserService.UserId;

            bool userExists = await _tripFlipDbContext.Users
                .AnyAsync(user => user.Id == currentUserId);
            if (!userExists)
            {
                throw new ArgumentException(ErrorConstants.NotAuthorized);
            }

            var routeToSubscribeTo = await _tripFlipDbContext
                .Routes
                .Include(route => route.RouteSubscribers)
                .Include(route => route.Trip)
                .ThenInclude(trip => trip.TripSubscribers)
                .FirstOrDefaultAsync(route => route.Id == routeId);

            // Validate route with a given id exists.
            EntityValidationHelper.ValidateEntityNotNull(
                routeToSubscribeTo, ErrorConstants.RouteNotFound);

            var currentUserAsTripSubscriber = routeToSubscribeTo
                .Trip
                .TripSubscribers
                .FirstOrDefault(tripSubscriber => tripSubscriber.UserId == currentUserId);

            // Validate current user is subscribed to a trip given route belongs to.
            if (currentUserAsTripSubscriber is null)
            {
                throw new ArgumentException(ErrorConstants.NotSubscriberOfTheTrip);
            }

            // Validate current user is not already subscribed to a given route.
            bool currentUserIsRouteSubscriber = currentUserAsTripSubscriber
                .RouteSubscriptions
                ?.Any(routeSubscriber => routeSubscriber.RouteId == routeId)
                ?? false;

            if (!currentUserIsRouteSubscriber)
            {
                routeToSubscribeTo.RouteSubscribers.Add(new RouteSubscriberEntity()
                {
                    RouteId = routeId,
                    TripSubscriberId = currentUserAsTripSubscriber.Id
                });

                await _tripFlipDbContext.SaveChangesAsync();
            }
        }

        public async Task SubscribeToTripAsync(int tripId)
        {
            var currentUserId = _currentUserService.UserId;

            var userExists = await _tripFlipDbContext.Users
                .AnyAsync(user => user.Id == currentUserId);

            if (!userExists)
            {
                throw new NotFoundException(ErrorConstants.NotAuthorized);
            }

            var tripEntity = await _tripFlipDbContext.Trips
                .Include(t => t.TripSubscribers)
                .FirstOrDefaultAsync(t => t.Id == tripId);

            EntityValidationHelper.ValidateEntityNotNull(tripEntity, ErrorConstants.TripNotFound);

            var isAlreadySubscriber = tripEntity.TripSubscribers
                .Any(subscriber => subscriber.UserId == currentUserId);

            if (isAlreadySubscriber)
            {
                throw new ArgumentException(ErrorConstants.IsAlreadyTripSubscriber);
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

        public async Task UnsubscribeFromTripAsync(int tripId)
        {
            var currentUserId = _currentUserService.UserId;

            var userExists = await _tripFlipDbContext.Users
                .AnyAsync(user => user.Id == currentUserId);

            if (!userExists)
            {
                throw new NotFoundException(ErrorConstants.NotAuthorized);
            }

            var tripSubscriberEntities = await _tripFlipDbContext.TripSubscribers
                .Include(tripSubscriber => tripSubscriber.TripRoles)
                .Where(tripSubscriber => tripSubscriber.TripId == tripId)
                .ToListAsync();

            var currentUserTripSubscriber = tripSubscriberEntities
                .FirstOrDefault(tripSubscriber => tripSubscriber.UserId == currentUserId);

            EntityValidationHelper.ValidateEntityNotNull(currentUserTripSubscriber, ErrorConstants.NotSubscriberOfTheTrip);

            var isCurrentUserTripAdmin = currentUserTripSubscriber.TripRoles
                .Any(tripSubscriberRole => tripSubscriberRole.TripRoleId == (int) TripRoles.Admin);

            if (isCurrentUserTripAdmin)
            {
                ValidateNotSingleTripAdminWhenUnsubscribe(tripSubscriberEntities);
            }

            _tripFlipDbContext.TripSubscribers.Remove(currentUserTripSubscriber);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task UnsubscribeFromRouteAsync(int routeId)
        {
            var currentUserId = _currentUserService.UserId;

            var userExists = await _tripFlipDbContext.Users
                .AnyAsync(user => user.Id == currentUserId);

            if (!userExists)
            {
                throw new NotFoundException(ErrorConstants.NotAuthorized);
            }

            var routeSubscriberEntity = await _tripFlipDbContext.RouteSubscribers
                .FirstOrDefaultAsync(routeSubscriber => 
                    routeSubscriber.TripSubscriber.UserId == currentUserId && 
                    routeSubscriber.RouteId == routeId);

            EntityValidationHelper.ValidateEntityNotNull(routeSubscriberEntity, 
                ErrorConstants.NotSubscriberOfTheRoute);

            _tripFlipDbContext.RouteSubscribers.Remove(routeSubscriberEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TripWithRoutesAndUserRolesDto>> GetAllSubscribedTripsAsync()
        {
            var currentUserId = _currentUserService.UserId;

            var userExists = await _tripFlipDbContext.Users
                .AnyAsync(user => user.Id == currentUserId);

            if (!userExists)
            {
                throw new NotFoundException(ErrorConstants.NotAuthorized);
            }

            var tripSubscriberEntities = await _tripFlipDbContext.TripSubscribers
                .Include(tripSubscriberEntity => tripSubscriberEntity.Trip)
                    .ThenInclude(tripEntity => tripEntity.Routes)
                        .ThenInclude(routeEntity => routeEntity.TaskLists)
                            .ThenInclude(taskListEntity => taskListEntity.Tasks)
                                .ThenInclude(taskEntity => taskEntity.TaskAssignees)
                                    .ThenInclude(taskAssigneeEntity => taskAssigneeEntity.RouteSubscriber)
                .Include(tripSubscriberEntity => tripSubscriberEntity.Trip)
                    .ThenInclude(tripEntity => tripEntity.Routes)
                        .ThenInclude(routeEntity => routeEntity.ItemLists)
                            .ThenInclude(itemListEntity => itemListEntity.Items)
                                .ThenInclude(itemEntity => itemEntity.ItemAssignees)
                                    .ThenInclude(itemAssigneeEntity => itemAssigneeEntity.RouteSubscriber)
                .Include(tripSubscriberEntity => tripSubscriberEntity.Trip)
                    .ThenInclude(tripEntity => tripEntity.Routes)
                        .ThenInclude(routeEntity => routeEntity.RoutePoints)
                .Include(tripSubscriberEntity => tripSubscriberEntity.TripRoles)
                    .ThenInclude(tripSubscriberRoleEntity => tripSubscriberRoleEntity.TripRole)
                .Where(tripSubscriberEntity => tripSubscriberEntity.UserId == currentUserId)
                .ToListAsync();

            var tripWithRoutesDto = _mapper.Map<List<TripWithRoutesAndUserRolesDto>>(tripSubscriberEntities);

            return tripWithRoutesDto;
        }

        public async Task<IEnumerable<UserEntity>> GetUsersWithBirthdayTodayAsync()
        {
            int currentDay = DateTimeOffset.Now.Day;
            int currentMonth = DateTimeOffset.Now.Month;

            var usersWithBirthdays = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .Where(user => user.BirthDate.Value.Day == currentDay &&
                    user.BirthDate.Value.Month == currentMonth)
                .ToListAsync();

            return usersWithBirthdays;
        }

        /// <summary>
        /// Validates whether there is more than one admin
        /// among trip subscribers.
        /// </summary>
        /// <param name="tripSubscriberEntitiesWithRoles">Collection of trip subscribers
        /// including their roles.</param>
        private void ValidateNotSingleTripAdminWhenUnsubscribe(
            ICollection<TripSubscriberEntity> tripSubscriberEntitiesWithRoles)
        {
            var tripAdminsCount = tripSubscriberEntitiesWithRoles
                    .Select(tripSubscriber => tripSubscriber.TripRoles)
                    .Count(roles =>
                        roles.Any(role => role.TripRoleId == (int)TripRoles.Admin));

            // If there is the only one admin trip, no permission to delete trip.
            if (tripAdminsCount == Constants.MinimumTripAdminCount)
            {
                throw new ArgumentException(ErrorConstants.SingleAdminTryToUnsubscribeFromTrip);
            }
        }
    }
}
