using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Interfaces;

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
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var registerUserDto = GetRegisterUserDto(email: ValidUser.Email);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => 
                await userService.RegisterAsync(registerUserDto));
        }

        [TestMethod]
        public async Task ChangePasswordAsync_GivenNonExistentUser_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();
            CurrentUserService = CreateCurrentUserService(InvalidUser.Id, InvalidUser.Email);
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var changePasswordDto = GetChangeUserPasswordDto();

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.ChangePasswordAsync(changePasswordDto));
        }

        [TestMethod]
        public async Task ChangePasswordAsync_GivenIncorrectPassword_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            var jwtConfiguration = CreateJwtConfiguration();
            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var incorrectPassword = "Incorrect@1pass";
            var changePasswordDto = GetChangeUserPasswordDto(oldPassword: incorrectPassword);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.ChangePasswordAsync(changePasswordDto));
        }

        [TestMethod]
        public async Task GetAllByTripIdAndCategorizeByRoleAsync_GivenIncorrectTripId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentTripId = 1;
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GetAllByTripIdAndCategorizeByRoleAsync(nonExistentTripId));
        }

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenNonExistentUserToGrantRolesId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentUserId = InvalidUser.Id;
            var jwtConfiguration = CreateJwtConfiguration();

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var grantTripRolesDto = GetGrantTripRolesDto(tripRoleIds: ValidTripRoleIds,
                userId: nonExistentUserId);
            
            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantTripRoleAsync(grantTripRolesDto));
        }

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenNonExistentTripId_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();
            var invalidTripId = 1000;

            Seed(TripFlipDbContext, ValidUser);
            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var grantTripRolesDto = GetGrantTripRolesDto(tripRoleIds: ValidTripRoleIds,
                userId: ValidUser.Id, tripId: invalidTripId);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantTripRoleAsync(grantTripRolesDto));
        }

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenNonExistentCurrentUser_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, TripEntityToSeed);

            CurrentUserService = CreateCurrentUserService(InvalidUser.Id, InvalidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var grantTripRolesDto = GetGrantTripRolesDto(tripRoleIds: ValidTripRoleIds,
                userId: ValidUser.Id);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantTripRoleAsync(grantTripRolesDto));
        }

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenCurrentUserNotTripAdmin_ExceptionThrown()
        {
            // Arrange.
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

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.GrantTripRoleAsync(grantTripRolesDto));
        }

        [TestMethod]
        public async Task SubscribeToRouteAsync_GivenNonExistentCurrentUser_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            CurrentUserService = CreateCurrentUserService(InvalidUser.Id,
                InvalidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.SubscribeToRouteAsync(RouteEntityToSeed.Id));
        }

        [TestMethod]
        public async Task SubscribeToRouteAsync_GivenNonExistentRouteId_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();
            var nonExistentRouteId = 1000;

            Seed(TripFlipDbContext, ValidUser);
            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.SubscribeToRouteAsync(nonExistentRouteId));
        }

        [TestMethod]
        public async Task SubscribeToRouteAsync_GivenNotSubscribedToTripCurrentUser_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ExistentButNotSubscribedToTripUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);

            // Trip subscribers are not seeded, so ValidUser can be used in this test.
            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.SubscribeToRouteAsync(RouteEntityToSeed.Id));
        }

        [TestMethod]
        public async Task UnsubscribeFromTripAsync_GivenNonExistentCurrentUser_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            CurrentUserService = CreateCurrentUserService(InvalidUser.Id, InvalidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.UnsubscribeFromTripAsync(TripEntityToSeed.Id));
        }

        [TestMethod]
        public async Task UnsubscribeFromTripAsync_GivenNotTripSubscriberCurrentUser_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ExistentButNotSubscribedToTripUser);

            CurrentUserService = CreateCurrentUserService(
                ExistentButNotSubscribedToTripUser.Id, 
                ExistentButNotSubscribedToTripUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.UnsubscribeFromTripAsync(TripEntityToSeed.Id));
        }

        [TestMethod]
        public async Task UnsubscribeFromTripAsync_GivenCurrentUserSingleTripAdmin_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.UnsubscribeFromTripAsync(TripEntityToSeed.Id));
        }

        [TestMethod]
        public async Task AuthorizeAsync_NonExistentUserEmail_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var loginDto = GetLoginDto();

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.AuthorizeAsync(loginDto));
        }

        [TestMethod]
        public async Task AuthorizeAsync_WrongPassword_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ValidUser);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var wrongPassword = "incorrect";
            var loginDto = GetLoginDto(password: wrongPassword);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.AuthorizeAsync(loginDto));
        }

        [TestMethod]
        public async Task GetByIdAsync_NonExistentUserId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentUserId = InvalidUser.Id;

            var jwtConfiguration = CreateJwtConfiguration();

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GetByIdAsync(nonExistentUserId));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_NonExistentUserId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentUserId = InvalidUser.Id;

            var jwtConfiguration = CreateJwtConfiguration();

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.DeleteByIdAsync(nonExistentUserId));
        }

        [TestMethod]
        public async Task GrantRouteRoleAsync_NonExistentRoute_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();
            var invalidRouteId = 1;

            Seed(TripFlipDbContext, ValidUser);
            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var grantRouteRolesDto = GetGrantRouteRolesDto(routeRoleIds: ValidRouteRoleIds,
                userId: ValidUser.Id, routeId: invalidRouteId);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantRouteRoleAsync(grantRouteRolesDto));
        }

        [TestMethod]
        public async Task GrantRouteRoleAsync_UserToGrantRoleIsNotTripSubscriber_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var jwtConfiguration = CreateJwtConfiguration();

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var grantRouteRolesDto = GetGrantRouteRolesDto(routeRoleIds: ValidRouteRoleIds,
                userId: NotTripSubscriberUser.Id);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantRouteRoleAsync(grantRouteRolesDto));
        }

        [TestMethod]
        public async Task GrantRouteRoleAsync_CurrentUserIsNotTripAdmin_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, NotTripAdminUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(NotTripAdminUser.Id,
                NotTripAdminUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var grantRouteRolesDto = GetGrantRouteRolesDto(routeRoleIds: ValidRouteRoleIds,
                userId: ValidUser.Id);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await userService.GrantRouteRoleAsync(grantRouteRolesDto));
        }

        [TestMethod]
        public async Task GrantRouteRoleAsync_NonExistentCurrentUser_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, RouteEntityToSeed);

            CurrentUserService = CreateCurrentUserService(InvalidUser.Id, InvalidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var grantRouteRolesDto = GetGrantRouteRolesDto(routeRoleIds: ValidRouteRoleIds,
                userId: ValidUser.Id);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantRouteRoleAsync(grantRouteRolesDto));
        }

        [TestMethod]
        public async Task GrantRouteRoleAsync_CurrentUserIsNotTripSubscriber_ExceptionThrown()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, NotTripSubscriberUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);

            CurrentUserService = CreateCurrentUserService(NotTripSubscriberUser.Id,
                NotTripSubscriberUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var grantRouteRolesDto = GetGrantRouteRolesDto(routeRoleIds: ValidRouteRoleIds,
                userId: ValidUser.Id);

            // Act & Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await userService.GrantRouteRoleAsync(grantRouteRolesDto));
        }
    }
}
