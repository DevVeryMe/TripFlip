using System;
using System.Threading.Tasks;
using TripFlip.Services.Dto;
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
        /// Updates the User.
        /// </summary>
        /// <param name="updateUserDto">New User data with existing User id.</param>
        /// <returns>User DTO that represents the updated database entry.</returns>
        Task<UserDto> UpdateAsync(UpdateUserDto updateUserDto);

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
        /// Deletes User.
        /// </summary>
        /// <param name="id">User id.</param>
        Task DeleteByIdAsync(Guid id);

        /// <summary>
        /// Grants a role to any user, who is subscriber of a trip,
        /// if the current one is granted with the admin role.
        /// </summary>
        /// <param name="grantSubscriberRoleDto">Data to with user id and role to grant.</param>
        Task GrantRoleAsync(GrantSubscriberRoleDto grantSubscriberRoleDto);

        /// <summary>
        /// Subscribes current user to a certain trip.
        /// </summary>
        /// <param name="tripId">Id of the trip to subscribe.</param>
        Task SubscribeToTripAsync(int tripId);
    }
}
