using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using TripFlip.Domain.Entities;

namespace TripFlip.Services.Helpers
{
    public static class PasswordHasherHelper
    {
        /// <summary>
        /// Returns a hashed representation of the supplied <paramref name="password"/>
        /// for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose password is to be hashed.</param>
        /// <param name="password">The password to hash.</param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException();
            }

            var passwordHasher = new PasswordHasher<UserEntity>();
            var user = new UserEntity();
            var hashedPassword = passwordHasher.HashPassword(user, password);

            return hashedPassword;
        }
    }
}
