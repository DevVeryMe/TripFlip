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

            // Type parameter do not affect on the behaviour of hasher and
            // is used only to implement custom hashers, so
            // in this case no matter with which class PasswordHasher 
            // is typed. That's why parameter user is null.
            var passwordHasher = new PasswordHasher<UserEntity>();
            var hashedPassword = passwordHasher.HashPassword(user: null, password: password);

            return hashedPassword;
        }
    }
}
