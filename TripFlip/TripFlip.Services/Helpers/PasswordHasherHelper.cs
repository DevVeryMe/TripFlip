using Microsoft.AspNetCore.Identity;
using TripFlip.Domain.Entities;

namespace TripFlip.Services.Helpers
{
    public static class PasswordHasherHelper
    {
        /// <summary>
        /// Returns a hashed representation of the supplied <paramref name="password"/>
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>Password hash.</returns>
        public static string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<UserEntity>();
            var user = new UserEntity();
            var hashedPassword = passwordHasher.HashPassword(user, password);

            return hashedPassword;
        }
    }
}
