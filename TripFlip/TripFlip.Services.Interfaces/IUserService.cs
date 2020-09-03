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
        /// Logs User in.
        /// </summary>
        /// <param name="loginDto">User credentials to log in.</param>
        /// <returns>User DTO that represents the authenticated User.</returns>
        Task<AuthenticatedUserDto> LoginAsync(LoginDto loginDto);

        /// <summary>
        /// Gets User with the given id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User DTO that represents the database entry with the given id.</returns>
        Task<UserDto> GetByIdAsync(int id);

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <param name="paginationDto">Pagination parameters.</param>
        /// <param name="searchString">String to filter Users.</param>
        /// <returns>Paged list of User DTOs that represent the database entries.</returns>
        Task<PagedList<UserDto>> GetAllByTripIdAsync(string searchString,
            PaginationDto paginationDto);

        /// <summary>
        /// Deletes User.
        /// </summary>
        /// <param name="id">User id.</param>
        Task DeleteByIdAsync(int id);
    }
}
