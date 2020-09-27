using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.RouteServiceTests
{
    [TestClass]
    public class RouteServiceNegativeTests : TestRouteServiceBase
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
        public async Task GetByIdAsync_GivenNotValidId_ExceptionThrown()
        {
            // Arrange
            var invalidId = 2;

            var routeService = new RouteService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.GetByIdAsync(invalidId));
        }

        [TestMethod]
        public async Task CreateAsync_NonExistentTripId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentTripId = 1;

            var createRouteDto = GetCreateRouteDto(tripId: nonExistentTripId);
            var routeService = new RouteService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.CreateAsync(createRouteDto));
        }

        [TestMethod]
        public async Task CreateAsync_CurrentUserNotTripAdmin_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var createRouteDto = GetCreateRouteDto();
            var routeService = new RouteService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await routeService.CreateAsync(createRouteDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task CreateAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            var createRouteDto = GetCreateRouteDto();
            var routeService = new RouteService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.CreateAsync(createRouteDto), displayName);
        }

        private static IEnumerable<object[]> GetCurrentUserServiceInvalidData()
        {
            yield return new object[]
            {
                "Test case 1: CreateAsync_GivenNotExistentCurrentUser_ExceptionThrown",
                CreateCurrentUserService(NonExistentUser.Id,
                    NonExistentUser.Email)
            };

            yield return new object[]
            {
                "Test case 2: CreateAsync_GivenCurrentUser" +
                "NotSubscribedToTrip_ExceptionThrown",
                CreateCurrentUserService(NotTripSubscriberUser.Id,
                    NotTripSubscriberUser.Email)
            };
        }
    }
}
