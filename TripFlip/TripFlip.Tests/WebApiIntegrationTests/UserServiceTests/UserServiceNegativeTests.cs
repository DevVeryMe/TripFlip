using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;

namespace WebApiIntegrationTests.UserServiceTests
{
    [TestClass]
    public class UserServiceNegativeTests : TestUserServiceBase
    {
        [TestInitialize]
        public void Initialize()
        {
            TripFlipDbContext = CreateDbContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            TripFlipDbContext.Dispose();
        }

        [TestMethod]
        public async Task RegisterAsync_GivenExistentUser_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, ValidUser);
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var registerUserDto = GetRegisterUserDto(email: ValidUser.Email);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => 
                await userService.RegisterAsync(registerUserDto));
        }

        [TestMethod]
        public async Task ChangePasswordAsync_GivenNonExistentUser_ExceptionThrown()
        {
            // Arrange
            var jwtConfiguration = CreateJwtConfiguration();
            CurrentUserService = CreateCurrentUserService(InvalidUser.Id, InvalidUser.Email);
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var changePasswordDto = GetChangeUserPasswordDto();

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.ChangePasswordAsync(changePasswordDto));
        }

        [TestMethod]
        public async Task ChangePasswordAsync_GivenIncorrectPassword_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, ValidUser);
            var jwtConfiguration = CreateJwtConfiguration();
            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var incorrectPassword = "Incorrect@1pass";
            var changePasswordDto = GetChangeUserPasswordDto(oldPassword: incorrectPassword);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.ChangePasswordAsync(changePasswordDto));
        }

        [TestMethod]
        public async Task GetAllByTripIdAndCategorizeByRoleAsync_GivenIncorrectTripId_ExceptionThrown()
        {
            // Arrange
            var nonExistentTripId = 1;
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GetAllByTripIdAndCategorizeByRoleAsync(nonExistentTripId));
        }
    }
}
