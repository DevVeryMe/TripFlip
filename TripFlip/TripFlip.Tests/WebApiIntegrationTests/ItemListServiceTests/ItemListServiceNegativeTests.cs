using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.ItemListServiceTests
{
    [TestClass]
    public class ItemListServiceNegativeTests : TestItemListServiceBase
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
        public async Task CreateAsync_GivenNonExistentRouteId_ExceptionThrown()
        {
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var nonExistentRouteId = 2;
            var createItemListDto = GetCreateItemListDto(routeId: nonExistentRouteId);
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemListService.CreateAsync(createItemListDto));
        }

        [TestMethod]
        public async Task CreateAsync_GivenCurrentUserNotRouteAdmin_ExceptionThrown()
        {
            Seed(TripFlipDbContext, NotRouteAdminRoleUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntityToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(NotRouteAdminRoleUser.Id,
                NotRouteAdminRoleUser.Email);
            var createItemListDto = GetCreateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemListService.CreateAsync(createItemListDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task CreateAsync_GivenNotValidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntityToSeed);

            CurrentUserService = currentUserService;
            var createItemListDto = GetCreateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemListService.CreateAsync(createItemListDto));
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

            yield return new object[]
            {
                "Test case 3: CreateAsync_GivenCurrentUser" +
                "NotSubscribedToRoute_ExceptionThrown",
                CreateCurrentUserService(NotRouteSubscriberUser.Id,
                    NotRouteSubscriberUser.Email)
            };
        }
    }
}
