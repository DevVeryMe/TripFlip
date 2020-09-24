using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto.ItemListDtos;
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
        public async Task Test_CreateItemList_Given_Non_existent_RouteId_should_be_failed()
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
        public async Task Test_CreateItemList_Given_CurrentUser_Not_Route_Admin_should_be_failed()
        {
            Seed(TripFlipDbContext, NotRouteAdminRoleUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntityToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(NotRouteAdminRoleUser.Id,
                NotRouteSubscriberUser.Email);
            var createItemListDto = GetCreateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemListService.CreateAsync(createItemListDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserData), DynamicDataSourceType.Method)]
        public async Task Test_CreateItemList_Given_Not_valid_CurrentUser_should_be_failed(
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

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            var createItemListDto = GetCreateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, currentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemListService.CreateAsync(createItemListDto));
        }

        private static IEnumerable<object[]> GetCurrentUserData()
        {
            yield return new object[]
            {
                "Test case 1: Test_CreateItemList_Given_Not_existent_CurrentUser_should_be_failed",
                CreateCurrentUserService(NonExistentUser.Id,
                    NonExistentUser.Email)
            };

            yield return new object[]
            {
                "Test case 2: Test_CreateItemList_Given__CurrentUser" +
                "_Not_subscribed_to_trip_should_be_failed",
                CreateCurrentUserService(NotTripSubscriberUser.Id,
                    NotTripSubscriberUser.Email)
            };

            yield return new object[]
            {
                "Test case 3: Test_CreateItemList_Given__CurrentUser" +
                "_Not_subscribed_to_route_should_be_failed",
                CreateCurrentUserService(NotRouteSubscriberUser.Id,
                    NotRouteSubscriberUser.Email)
            };
        }

        private CreateItemListDto GetCreateItemListDto(int routeId = 1,
            string title = "Title")
        {
            return new CreateItemListDto()
            {
                RouteId = routeId,
                Title = title
            };
        }
    }
}
