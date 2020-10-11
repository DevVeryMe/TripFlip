using System;
using System.Threading.Tasks;
using GoogleAuthentication.Services.Dtos;

namespace GoogleAuthentication.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Authorizes user with Google account.
        /// </summary>
        /// <returns>Authenticated user DTO containing user email and JWT.</returns>
        public Task<AuthenticatedUserDto> SignInWithGoogle();

        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>UserDto that represents database entry about user with specified id.</returns>
        public Task<UserDto> GetUserById(Guid id);
    }
}
