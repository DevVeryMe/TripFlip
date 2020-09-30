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
        public async Task GetAllByTripIdAsync_NonExistentTripId_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, RouteEntitiesToSeed);

            var routeService = new RouteService(TripFlipDbContext, Mapper,
                CurrentUserService);

            var nonExistentTripId = 1;
            var paginationDto = GetPaginationDto();
            string searchString = null;

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.GetAllByTripIdAsync(nonExistentTripId,
                    searchString, paginationDto));
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

        [TestMethod]
        public async Task UpdateAsync_NonExistentTripId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentTripId = 1;

            var updateRouteDto = Get_UpdateRouteDto(tripId: nonExistentTripId);

            var routeService = new RouteService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: null,
                currentUserService: null);

            // Act & Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.UpdateAsync(updateRouteDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task UpdateAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            var updateRouteDto = Get_UpdateRouteDto(tripId: TripEntityToSeed.Id);

            var routeService = new RouteService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act & Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.UpdateAsync(updateRouteDto), displayName);
        }

        [TestMethod]
        public async Task UpdateAsync_CurrentUserNotTripEditor_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);

            var updateRouteDto = Get_UpdateRouteDto(tripId: TripEntityToSeed.Id);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var routeService = new RouteService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act & Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await routeService.UpdateAsync(updateRouteDto));
        }

        [TestMethod]
        public async Task UpdateAsync_NonExistentRouteId_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var routeService = new RouteService(TripFlipDbContext, Mapper, CurrentUserService);

            int invalidRouteId = 1;

            var updateRouteDto = Get_UpdateRouteDto(
                tripId: TripEntityToSeed.Id,
                id: invalidRouteId);

            // Act & Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.UpdateAsync(updateRouteDto));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_NonExistentRouteId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentRouteId = 1;

            var routeService = new RouteService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: null,
                currentUserService: null);

            // Act & Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.DeleteByIdAsync(nonExistentRouteId));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task DeleteByIdAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            var routeService = new RouteService(TripFlipDbContext, Mapper,
                CurrentUserService);

            int validRouteId = RouteEntityToSeed.Id;

            // Act & Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.DeleteByIdAsync(validRouteId), displayName);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_CurrentUserNotTripAdmin_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var routeService = new RouteService(TripFlipDbContext, Mapper, CurrentUserService);

            int validRouteId = RouteEntityToSeed.Id;

            // Act & Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await routeService.DeleteByIdAsync(validRouteId));
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
