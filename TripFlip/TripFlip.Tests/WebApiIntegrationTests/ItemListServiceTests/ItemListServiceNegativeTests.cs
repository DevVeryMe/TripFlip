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
        public async Task UpdateAsync_NonExistentItemList_ExceptionThrown()
        {
            // Arrange.
            var updateItemListDto = GetUpdateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemListService.UpdateAsync(updateItemListDto));
        }


        [TestMethod]
        public async Task UpdateAsync_CurrentUserNotRouteEditor_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, UserWithoutRouteRoles);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(UserWithoutRouteRoles.Id,
                UserWithoutRouteRoles.Email);

            var updateItemListDto = GetUpdateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemListService.UpdateAsync(updateItemListDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task UpdateAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = currentUserService;
            var updateItemListDto = GetUpdateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemListService.UpdateAsync(updateItemListDto), displayName);
        }

        [TestMethod]
        public async Task GetByIdAsync_NonExistentItemListId_ExceptionThrown()
        {
            // Arrange.
            // There is no ItemList entries in database, so any id will be non-existent.
            var nonExistentItemListId = 1;
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemListService.GetByIdAsync(nonExistentItemListId));
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
            Seed(TripFlipDbContext, UserWithoutRouteRoles);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(UserWithoutRouteRoles.Id,
                UserWithoutRouteRoles.Email);
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
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

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
