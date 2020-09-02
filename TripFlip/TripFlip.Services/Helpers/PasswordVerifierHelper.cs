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

            // Type parameter do not affect on the behaviour of hasher and
            // is used only to implement custom hashers, so
            // in this case no matter with which class PasswordHasher 
            // is typed. That's why parameter user is null.
            var passwordHasher = new PasswordHasher<UserEntity>();
            var result = passwordHasher.VerifyHashedPassword(user: null, 
                hashedPassword: storedPasswordHash, providedPassword: password);

            if (result == PasswordVerificationResult.Success ||
                result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                isVerified = true;
            }

            return isVerified;
        }
    }
}
