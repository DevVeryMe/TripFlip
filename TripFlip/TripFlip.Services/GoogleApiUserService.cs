using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class GoogleApiUserService : IGoogleApiUserService
    {
        private readonly TripFlipDbContext _tripFlipDbContext;

        /// <summary>
        /// Initializes db context field.
        /// </summary>
        /// <param name="tripFlipDbContext">Instance of db context.</param>
        public GoogleApiUserService(TripFlipDbContext tripFlipDbContext)
        {
            _tripFlipDbContext = tripFlipDbContext;
        }

        /// <summary>
        /// Registers new user, randomizing his password.
        /// </summary>
        /// <param name="email">Email to register user with.</param>
        /// <returns>Registered user entity.</returns>
        private async Task<UserEntity> RegisterAsync(string email)
        {
            var userToRegister = CreateUserEntityWithRandomPassword(email);

            await _tripFlipDbContext.Users.AddAsync(userToRegister);
            await _tripFlipDbContext.SaveChangesAsync();

            return userToRegister;
        }

        /// <summary>
        /// Creates a user entity with randomized password.
        /// </summary>
        /// <param name="email">Email to create user with.</param>
        /// <returns>Created user entity.</returns>
        private UserEntity CreateUserEntityWithRandomPassword(string email)
        {
            var password = PasswordGeneratorHelper.GeneratePassword(useLowercase: true,
                useUppercase: true, useNumbers: true, useSpecial: true, passwordSize: 50);

            var passwordHash = PasswordHasherHelper.HashPassword(password);

            return new UserEntity()
            {
                Email = email,
                PasswordHash = passwordHash
            };
        }
    }
}
