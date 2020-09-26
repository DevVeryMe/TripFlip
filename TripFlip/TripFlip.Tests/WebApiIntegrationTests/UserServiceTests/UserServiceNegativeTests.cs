using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto.UserDtos;

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

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenNonExistentUserToGrantRolesId_Exception_Thrown()
        {
            // Arrange
            var nonExistentUserId = InvalidUser.Id;
            var jwtConfiguration = CreateJwtConfiguration();

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var grantTripRolesDto = GetGrantTripRolesDto(tripRoleIds: ValidTripRoleIds,
                userId: nonExistentUserId);
            
            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantTripRoleAsync(grantTripRolesDto));
        }

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenNonExistentTripId_Exception_Thrown()
        {
            // Arrange
            var jwtConfiguration = CreateJwtConfiguration();
            var invalidTripId = 1000;

            Seed(TripFlipDbContext, ValidUser);
            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var grantTripRolesDto = GetGrantTripRolesDto(tripRoleIds: ValidTripRoleIds,
                userId: ValidUser.Id, tripId: invalidTripId);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantTripRoleAsync(grantTripRolesDto));
        }

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenNonExistentCurrentUser_Exception_Thrown()
        {
            // Arrange
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, TripEntityToSeed);

            CurrentUserService = CreateCurrentUserService(InvalidUser.Id, InvalidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var grantTripRolesDto = GetGrantTripRolesDto(tripRoleIds: ValidTripRoleIds,
                userId: ValidUser.Id);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantTripRoleAsync(grantTripRolesDto));
        }

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenCurrentUserNotTripAdmin_Exception_Thrown()
        {
            // Arrange
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, NotTripAdminUser);
            Seed(TripFlipDbContext, UserEntitiesToSeed);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(NotTripAdminUser.Id, 
                NotTripAdminUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var grantTripRolesDto = GetGrantTripRolesDto(tripRoleIds: ValidTripRoleIds,
                userId: ValidUser.Id);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.GrantTripRoleAsync(grantTripRolesDto));
        }
    }
}
