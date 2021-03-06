﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task GetAllByTaskListIdAsync_NonExistentItemListId_ExceptionThrown()
        {
            // Arrange.
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);

            var nonExistentItemListId = 1;
            var paginationDto = GetPaginationDto();
            string searchString = null;

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.GetAllByItemListIdAsync(nonExistentItemListId,
                    searchString, paginationDto));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_NonExistentItemId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentItemId = 1;
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.DeleteByIdAsync(nonExistentItemId));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_CurrentUserNotRouteAdmin_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var existentItemId = 1;
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemService.DeleteByIdAsync(existentItemId));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task DeleteByIdAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = currentUserService;

            var existentItemId = 1;
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.DeleteByIdAsync(existentItemId), displayName);
        }

        [TestMethod]
        public async Task CreateAsync_GivenNonExistentItemListId_ExceptionThrown()
        {
            // Arrange
            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var nonExistentItemListId = 2;

            var createItemDto = GetCreateItemDto(itemListId: nonExistentItemListId);
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.CreateAsync(createItemDto));
        }

        [TestMethod]
        public async Task CreateAsync_GivenCurrentUserNotRouteAdmin_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, RouteSubscriberWithoutRolesUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(RouteSubscriberWithoutRolesUser.Id,
                RouteSubscriberWithoutRolesUser.Email);
            var createItemDto = GetCreateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemService.CreateAsync(createItemDto));
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
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = currentUserService;
            var createItemDto = GetCreateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.CreateAsync(createItemDto));
        }

        [TestMethod]
        public async Task UpdateAsync_GivenNonExistentItemListId_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var nonExistentItemId = 2;

            var updateItemDto = GetUpdateItemDto(itemId: nonExistentItemId);
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.UpdateAsync(updateItemDto));
        }

        [TestMethod]
        public async Task UpdateAsync_GivenCurrentUserNotRouteAdmin_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, RouteSubscriberWithoutRolesUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(RouteSubscriberWithoutRolesUser.Id,
                RouteSubscriberWithoutRolesUser.Email);
            var updateItemDto = GetUpdateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemService.UpdateAsync(updateItemDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task UpdateAsync_GivenNotValidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            // Arrange
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
            var updateItemDto = GetUpdateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.UpdateAsync(updateItemDto));
        }

        [TestMethod]
        public async Task SetItemAssigneesAsync_GivenNonExistentItemId_ExceptionThrown()
        {
            // Arrange.
            var itemAssigneesDto = GetItemAssigneesDto(routeSubscriberIds:
                ValidRouteSubscribersToAssignToItem);
            var itemService = new ItemService(Mapper, TripFlipDbContext, null);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.SetItemAssigneesAsync(itemAssigneesDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task SetItemAssigneesAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            var itemAssigneesDto = GetItemAssigneesDto(routeSubscriberIds:
                ValidRouteSubscribersToAssignToItem);
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.SetItemAssigneesAsync(itemAssigneesDto), displayName);
        }

        [TestMethod]
        public async Task SetItemAssigneesAsync_CurrentUserNotRouteEditor_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, RouteSubscriberWithoutRolesUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            var notRouteEditorUserId = RouteSubscriberWithoutRolesUser.Id;
            var notRouteEditorUserEmail = RouteSubscriberWithoutRolesUser.Email;

            CurrentUserService = CreateCurrentUserService(notRouteEditorUserId,
                notRouteEditorUserEmail);

            var itemAssigneesDto = GetItemAssigneesDto(routeSubscriberIds:
                ValidRouteSubscribersToAssignToItem);
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemService.SetItemAssigneesAsync(itemAssigneesDto));
        }

        [TestMethod]
        public async Task SetItemAssigneesAsync_GivenNonExistentRouteSubscriberIds_ExceptionThrown()
        {
            // Arrange.
            var invalidRouteSubscriberIds = new List<int>()
            {
                10, 100, 1000
            };

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEditorRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var itemAssigneesDto = GetItemAssigneesDto(routeSubscriberIds:
                invalidRouteSubscriberIds);
            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemService.SetItemAssigneesAsync(itemAssigneesDto));
        }

        [TestMethod]
        public async Task GetByIdAsync_GivenNonExistentItemId_ExceptionThrown()
        {
            // Arrange.
            int nonExistenItemId = 1;

            var itemService = new ItemService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: null,
                currentUserService: null);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.GetByIdAsync(nonExistenItemId));
        }

        [TestMethod]
        public async Task UpdateCompletenessAsync_GivenNonExistentItemId_ExceptionThrown()
        {
            // Arrange.
            int nonExistenItemId = 1;

            var updateItemCompletenessDto = Get_UpdateItemCompletenessDto(itemId: nonExistenItemId);

            var itemService = new ItemService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: null,
                currentUserService: null);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.UpdateCompletenessAsync(updateItemCompletenessDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task UpdateCompletenessAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            int existingItemId = ItemEntityToSeed.Id;

            var updateItemCompletenessDto = Get_UpdateItemCompletenessDto(itemId: existingItemId);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await itemService.UpdateCompletenessAsync(updateItemCompletenessDto), displayName);
        }

        [TestMethod]
        public async Task UpdateCompletenessAsync_CurrentUserNotRouteEditor_ExceptionThrown()
        {
            // Arrange.
            var userThatIsRouteSubButNotRouteEditor = RouteSubscriberWithoutRolesUser;
            Seed(TripFlipDbContext, userThatIsRouteSubButNotRouteEditor);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(userThatIsRouteSubButNotRouteEditor.Id,
                userThatIsRouteSubButNotRouteEditor.Email);

            var itemService = new ItemService(Mapper, TripFlipDbContext, CurrentUserService);

            int existingItemId = ItemEntityToSeed.Id;

            var updateItemCompletenessDto = Get_UpdateItemCompletenessDto(itemId: existingItemId);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await itemService.UpdateCompletenessAsync(updateItemCompletenessDto));
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
