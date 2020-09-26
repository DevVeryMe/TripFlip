using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto.TripDtos;

namespace WebApiIntegrationTests.TripServiceTests
{
    [TestClass]
    public class TripServiceNegativeTests : TestTripServiceBase
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
        public async Task UpdateAsync_NonExistentTrip_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, UserEntityToSeed);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberAdminRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserServiceWithExistentUser();
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            var invalidTripId = 2;
            var updateTripDto = GetUpdateTripDto(invalidTripId);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await TripService.UpdateAsync(updateTripDto));
        }

        [TestMethod]
        public async Task UpdateAsync_CurrentUserNotTripAdmin_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, UserEntityToSeed);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntityToSeed);

            CurrentUserService = CreateCurrentUserServiceWithExistentUser();
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            var updateTripDto = GetUpdateTripDto();

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await TripService.UpdateAsync(updateTripDto));
        }

        [TestMethod]
        public async Task UpdateAsync_CurrentUserNotTripSubsriber_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, UserEntityToSeed);
            Seed(TripFlipDbContext, TripEntityToSeed);

            CurrentUserService = CreateCurrentUserServiceWithExistentUser();
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            var updateTripDto = GetUpdateTripDto();

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await TripService.UpdateAsync(updateTripDto));
        }

        [TestMethod]
        public async Task Test_CreateAsync_Given_Not_valid_CurrentUser_Data_Validation_should_be_failed()
        {
            Seed(TripFlipDbContext, UserEntityToSeed);
            Seed(TripFlipDbContext, TripRolesToSeed);

            var createTripDto = GetCreateTripDtoData();

            // Reset CurrentUserService and TripService with non existent user.
            CurrentUserService = CreateCurrentUserServiceWithNonExistentUser();
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(
                () => TripService.CreateAsync(createTripDto));
        }

        [TestMethod]
        public async Task GetByIdAsync_GivenNonExistentTripId_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, TripEntityToSeed);

            int nonExistentTripId = 2;

            TripService = new TripService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(
                () => TripService.GetByIdAsync(nonExistentTripId));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_CurrentUserNotTripSubscriber_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, TripEntityToSeed);

            Seed(TripFlipDbContext, UserEntityToSeed);
            CurrentUserService = CreateCurrentUserServiceWithExistentUser();

            TripService = new TripService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: CurrentUserService);

            int validTripId = 1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(
                () => TripService.DeleteByIdAsync(validTripId));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_CurrentUserNotAdmin_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, TripEntityToSeed);

            Seed(TripFlipDbContext, UserEntityToSeed);
            CurrentUserService = CreateCurrentUserServiceWithExistentUser();

            Seed(TripFlipDbContext, TripSubscriberEntityToSeed);

            TripService = new TripService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: CurrentUserService);

            int validTripId = 1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => TripService.DeleteByIdAsync(validTripId));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_GivenNonExistentTripId_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, UserEntityToSeed);
            CurrentUserService = CreateCurrentUserServiceWithExistentUser();

            TripService = new TripService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: CurrentUserService);

            int invalidTripId = 2;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(
                () => TripService.DeleteByIdAsync(invalidTripId));
        }

        private static CreateTripDto GetCreateTripDtoData()
        {
            return new CreateTripDto()
            {
                Title = "Trip",
                Description = "Description",
                StartsAt = DateTimeOffset.Parse("28/08/2030 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
                EndsAt = DateTimeOffset.Parse("30/11/2030 19:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
            };
        }
    }
}
