using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.ItemListServiceTests
{
    [TestClass]
    public class ItemListServiceNegativeTests : TestItemListServiceBase
    {
        [TestMethod]
        public async Task Test_CreateItemList_Given_Non_existent_RouteId_should_be_failed()
        {
            var tripFlipDbContext = CreateDbContext();
            Seed(tripFlipDbContext, CorrectUser);
            Seed(tripFlipDbContext, TripEntityToSeed);
            Seed(tripFlipDbContext, RouteEntityToSeed);

            CurrentUserService = CreateCurrentUserServiceWithExistentUser();
            var createItemListDto = GetCreateItemListDto(routeId: 2);
            var itemListService = new ItemListService(tripFlipDbContext, Mapper, CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemListService.CreateAsync(createItemListDto));
        }

        [TestMethod]
        public async Task Test_CreateItemList_Given_CurrentUser_Not_Route_Admin_should_be_failed()
        {
            var tripFlipDbContext = CreateDbContext();

            Seed(tripFlipDbContext, NotRouteAdminRoleUser);
            Seed(tripFlipDbContext, TripEntityToSeed);
            Seed(tripFlipDbContext, RouteEntityToSeed);
            Seed(tripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(tripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(tripFlipDbContext, RouteRoleEntityToSeed);
            Seed(tripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(NotRouteAdminRoleUser.Id,
                NotRouteSubscriberUser.Email);
            var createItemListDto = GetCreateItemListDto();
            var itemListService = new ItemListService(tripFlipDbContext, Mapper, CurrentUserService);

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemListService.CreateAsync(createItemListDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserData), DynamicDataSourceType.Method)]
        public async Task Test_CreateItemList_Given_Not_valid_CurrentUser_should_be_failed(
            string displayName, ICurrentUserService currentUserService)
        {
            var tripFlipDbContext = CreateDbContext();

            Seed(tripFlipDbContext, NonExistentUser, 
                NotRouteSubscriberUser, 
                NotTripSubscriberUser);
            Seed(tripFlipDbContext, TripEntityToSeed);
            Seed(tripFlipDbContext, RouteEntityToSeed);
            Seed(tripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(tripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(tripFlipDbContext, RouteRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserServiceWithExistentUser();
            var createItemListDto = GetCreateItemListDto();
            var itemListService = new ItemListService(tripFlipDbContext, Mapper, currentUserService);

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
