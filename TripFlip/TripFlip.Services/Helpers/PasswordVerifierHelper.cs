using Microsoft.AspNetCore.Identity;
using TripFlip.Domain.Entities;

namespace TripFlip.Services.Helpers
{
    public static class PasswordVerifierHelper
    {
        /// <summary>
        ///  Gets a value indicating the result of a password hash comparison.
        /// </summary>
        /// <param name="password">The password supplied for comparison.</param>
        /// <param name="storedPasswordHash">The hash value for a user's stored password.</param>
        /// <returns>Bool value indicating the result of a password hash comparison.</returns>
        public static bool VerifyPassword(string password, string storedPasswordHash)
        {
            var isVerified = false;
            var user = new UserEntity();
            var passwordHasher = new PasswordHasher<UserEntity>();

            var result = passwordHasher.VerifyHashedPassword(user, storedPasswordHash, password);

            if (result == PasswordVerificationResult.Success ||
                result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                isVerified = true;
            }

            return isVerified;
        }
    }
}
