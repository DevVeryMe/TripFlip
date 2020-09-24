using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.ItemServiceTests
{
    [TestClass]
    public class ItemServiceNegativeTests : TestItemServiceBase
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
        public async Task Test_CreateItem_Given_Non_existent_ItemListId_should_be_failed()
        {
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var nonExistentItemListId = 2;

            var createItemDto = GetCreateItemDto(itemListId: nonExistentItemListId);
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.CreateAsync(createItemDto));
        }

        [TestMethod]
        public async Task Test_CreateItemList_Given_CurrentUser_Not_Route_Admin_should_be_failed()
        {
            Seed(TripFlipDbContext, NotRouteAdminRoleUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntityToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(NotRouteAdminRoleUser.Id,
                NotRouteAdminRoleUser.Email);
            var createItemDto = GetCreateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemService.CreateAsync(createItemDto));
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
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            var createItemDto = GetCreateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.CreateAsync(createItemDto));
        }

        private static IEnumerable<object[]> GetCurrentUserData()
        {
            yield return new object[]
            {
                "Test case 1: Test_CreateItem_Given_Not_existent_CurrentUser_should_be_failed",
                CreateCurrentUserService(NonExistentUser.Id,
                    NonExistentUser.Email)
            };

            yield return new object[]
            {
                "Test case 2: Test_CreateItem_Given__CurrentUser" +
                "_Not_subscribed_to_trip_should_be_failed",
                CreateCurrentUserService(NotTripSubscriberUser.Id,
                    NotTripSubscriberUser.Email)
            };

            yield return new object[]
            {
                "Test case 3: Test_CreateItem_Given__CurrentUser" +
                "_Not_subscribed_to_route_should_be_failed",
                CreateCurrentUserService(NotRouteSubscriberUser.Id,
                    NotRouteSubscriberUser.Email)
            };
        }

        private CreateItemDto GetCreateItemDto(int itemListId = 1, string title = "Title", 
            string comment = "Comment", string quantity = "Quantity")
        {
            return new CreateItemDto()
            {
                ItemListId = itemListId,
                Title = title,
                Comment = comment,
                Quantity = quantity
            };
        }

    }
}
