using AutoMapper;
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
                .Where(requestedId => existingApplicationRolesIds.Contains(requestedId));

            // Add requested set of roles to user.
            bool collectionHasRolesToAdd = grantApplicationRolesDto.ApplicationRoleIds.Count() > 0;
            if (collectionHasRolesToAdd)
            {
                var rolesToAdd = new List<ApplicationUserRoleEntity>();

                foreach (var requestedRole in grantApplicationRolesDto.ApplicationRoleIds)
                {
                    rolesToAdd.Add(new ApplicationUserRoleEntity()
                    {
                        UserId = userToGrantRoles.Id,
                        ApplicationRoleId = (int)requestedRole
                    });
                }

                _tripFlipDbContext.ApplicationUsersRoles.AddRange(rolesToAdd);
            }
            
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task GrantTripRoleAsync(GrantSubscriberRoleDto grantSubscriberRoleDto)
        {
            var currentUserId = _currentUserService.UserId;

            // Validate user-to-grant-role-to exists.
            var userToGrantRole = await _tripFlipDbContext.Users
                .SingleOrDefaultAsync(user => user.Id == grantSubscriberRoleDto.UserId);
            ValidateUserEntityNotNull(userToGrantRole);

            // Validate trip exists.
            var trip = await _tripFlipDbContext.Trips
                .Include(t => t.TripSubscribers)
                .ThenInclude(subscribers => subscribers.TripRoles)
                .FirstOrDefaultAsync(t => t.Id == grantSubscriberRoleDto.TripId);
            EntityValidationHelper.
                ValidateEntityNotNull<TripEntity>(trip, ErrorConstants.TripNotFound);

            // Validate current user is trip admin.
            await EntityValidationHelper.ValidateCurrentUserIsTripAdminAsync(
                _currentUserService, _tripFlipDbContext, grantSubscriberRoleDto.TripId);

            // Remove invalid values from requested role id collection.
            var realTripRolesIds = (IEnumerable<int>) Enum.GetValues(typeof(TripRoles));
            grantSubscriberRoleDto.TripRoleIds = grantSubscriberRoleDto
                .TripRoleIds
                .Distinct()
                .Where(requestedId => realTripRolesIds.Contains(requestedId));

            var tripSubscriber = trip.TripSubscribers
                .FirstOrDefault(subscribers => subscribers.UserId == grantSubscriberRoleDto.UserId);

            // Subscribe given user to trip if he's not subscribed.
            if (tripSubscriber is null)
            {
                tripSubscriber = new TripSubscriberEntity()
                {
                    TripId = grantSubscriberRoleDto.TripId,
                    UserId = grantSubscriberRoleDto.UserId
                };
            }

            // Remove subscriber's current set of roles.
            if (!(tripSubscriber.TripRoles is null))
            {
                _tripFlipDbContext.TripSubscribersRoles.RemoveRange(
                    tripSubscriber.TripRoles);
            }

            // Add requested set of roles to subscriber.
            bool collectionHasRolesToAdd = grantSubscriberRoleDto.TripRoleIds.Count() > 0;
            if (collectionHasRolesToAdd)
            {
                var rolesToAdd = new List<TripSubscriberRoleEntity>();

                foreach (int requestedRoleId in grantSubscriberRoleDto.TripRoleIds)
                {
                    rolesToAdd.Add(new TripSubscriberRoleEntity()
                    {
                        TripSubscriber = tripSubscriber,
                        TripRoleId = requestedRoleId
                    });
                }

                _tripFlipDbContext.TripSubscribersRoles.AddRange(rolesToAdd);
            }
            

            await _tripFlipDbContext.SaveChangesAsync();
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
                .Include(tripSubscriberEntity => tripSubscriberEntity.Trip)
                    .ThenInclude(tripEntity => tripEntity.Routes)
                        .ThenInclude(routeEntity => routeEntity.ItemLists)
                            .ThenInclude(itemListEntity => itemListEntity.Items)
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

        /// <summary>
        /// Checks if the given <see cref="UserEntity"/> is not null. If null,
        /// then throws an <see cref="NotFoundException"/> with a corresponding message.
        /// </summary>
        /// <param name="userEntity">Object that should be checked.</param>
        private void ValidateUserEntityNotNull(UserEntity userEntity)
        {
            if (userEntity is null)
            {
                throw new NotFoundException(ErrorConstants.UserNotFound);
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
                .Select(role => new Claim(ClaimTypes.Role, role.ApplicationRole.Name));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userIncludingRoles.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userIncludingRoles.Email)
            };

            claims.AddRange(roles);

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
