using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Describes supported operations with User entity.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new User.
        /// </summary>
        /// <param name="registerUserDto">Data to register a new User.</param>
        /// <returns>User DTO that represents the new entry that was added to database.</returns>
        Task<UserDto> RegisterAsync(RegisterUserDto registerUserDto);

        /// <summary>
        /// Authorizes User.
        /// </summary>
        /// <param name="loginDto">User credentials to log in.</param>
        /// <returns>User DTO that represents the authenticated User.</returns>
        Task<AuthenticatedUserDto> AuthorizeAsync(LoginDto loginDto);

        /// <summary>
        /// Updates authorized User.
        /// </summary>
        /// <param name="updateUserDto">New User data.</param>
        /// <returns>User DTO that represents the updated database entry.</returns>
        Task<UserDto> UpdateUserProfileAsync(UpdateUserProfileDto updateUserDto);

        /// <summary>
        /// Changes password of authorized user.
        /// </summary>
        /// <param name="changeUserPasswordDto">DTO that contains both 
        /// old and new user passwords.</param>
        Task ChangePasswordAsync(ChangeUserPasswordDto changeUserPasswordDto);

        /// <summary>
        /// Gets User with the given id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User DTO that represents the database entry with the given id.</returns>
        Task<UserDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <param name="paginationDto">Pagination parameters.</param>
        /// <param name="searchString">String to filter Users.</param>
        /// <returns>Paged list of User DTOs that represent the database entries.</returns>
        Task<PagedList<UserDto>> GetAllAsync(string searchString,
            PaginationDto paginationDto);

        /// <summary>
        /// Gets all Users by trip Id, and categorized by roles.
        /// </summary>
        /// <param name="tripId">Id of a trip to find users with.</param>
        /// <returns>User DTO that represent all users that are subscribed to a given trip. 
        /// All users are categorized by their trip roles.</returns>
        Task<UsersByTripAndCategorizedByRoleDto> GetAllByTripIdAndCategorizeByRoleAsync(
            int tripId);

        /// <summary>
        /// Deletes User.
        /// </summary>
        /// <param name="id">User id.</param>
        Task DeleteByIdAsync(Guid id);

        /// <summary>
        /// Grants an application roles to any user,
        /// if the current one is granted with super admin role.
        /// </summary>
        /// <param name="grantApplicationRolesDto">Data to with user id and roles to grant.</param>
        Task GrantApplicationRoleAsync(GrantApplicationRolesDto grantApplicationRolesDto);

        /// <summary>
        /// Grants a role to any user, who is subscriber of a trip,
        /// if the current one is granted with the admin role.
        /// </summary>
        /// <param name="grantTripRolesDto">Data to with user id and role to grant.</param>
        Task GrantTripRoleAsync(GrantTripRolesDto grantTripRolesDto);

        /// <summary>
        /// Grants a route role to any user, who is the subscriber of a route,
        /// if the current one is granted with the trip admin role.
        /// </summary>
        /// <param name="grantRouteRolesDto">Data with user id and list of roles to grant.</param>
        Task GrantRouteRoleAsync(GrantRouteRolesDto grantRouteRolesDto);

        /// <summary>
        /// Subscribes current user to a certain route.
        /// </summary>
        /// <param name="routeId">Id of the route to subscribe to.</param>
        Task SubscribeToRouteAsync(int routeId);

        /// <summary>
        /// Subscribes current user to a certain trip.
        /// </summary>
        /// <param name="tripId">Id of the trip to subscribe.</param>
        Task SubscribeToTripAsync(int tripId);

        /// <summary>
        /// Unsubscribes current user from a trip.
        /// </summary>
        /// <param name="tripId">Id of the trip to unsubscribe from.</param>
        Task UnsubscribeFromTripAsync(int tripId);

        /// <summary>
        /// Unsubscribes current user from a route.
        /// </summary>
        /// <param name="routeId">Id of the route to unsubscribe from.</param>
        Task UnsubscribeFromRouteAsync(int routeId);

        /// <summary>
        /// Gets all trips, which are subscribed by current user, with included
        /// routes with points, task and item lists with tasks and items, user roles
        /// in this trips.
        /// </summary>
        Task<IEnumerable<TripWithRoutesAndUserRolesDto>> GetAllSubscribedTripsAsync();

        /// <summary>
        /// Makes a database query and returns a collection of users that have their 
        /// birthday today (current day and month matches day and month inside 
        /// <see cref="UserEntity.DateCreated"/> property.
        /// </summary>
        /// <returns>A collection users that have their birthday today.</returns>
        Task<IEnumerable<UserEntity>> GetUsersWithBirthdayTodayAsync();
    }
}
