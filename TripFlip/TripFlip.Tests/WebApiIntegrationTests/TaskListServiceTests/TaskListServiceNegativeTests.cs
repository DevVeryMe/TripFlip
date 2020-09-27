using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.TaskListServiceTests
{
    [TestClass]
    public class TaskListServiceNegativeTests : TestTaskListServiceBase
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
        public async Task GetByIdAsync_NonExistentTaskListId_ExceptionThrown()
        {
            // Arrange.
            var nonExistentTaskListId = 1;

            var taskListService = new TaskListService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskListService.GetByIdAsync(nonExistentTaskListId));
        }

        [TestMethod]
        public async Task GetAllByRouteIdAsync_GivenInvalidRouteId_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, TaskListEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var taskListService = new TaskListService(TripFlipDbContext, 
                Mapper, CurrentUserService);
            var invalidRouteId = 2;
            var paginationDto = GetPaginationDto();
            string searchString = null;

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskListService.GetAllByRouteIdAsync(invalidRouteId, 
                    searchString, paginationDto));
        }

        [TestMethod]
        public async Task UpdateAsync_NonExistentTaskList_ExceptionThrown()
        {
            // Arrange.
            var updateTaskListDto = GetUpdateTaskListDto();
            var taskListService = new TaskListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskListService.UpdateAsync(updateTaskListDto));
        }

        [TestMethod]
        public async Task UpdateAsync_CurrentUserNotRouteEditor_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, UserWithoutRouteRoles);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(UserWithoutRouteRoles.Id,
                UserWithoutRouteRoles.Email);

            var updateTaskListDto = GetUpdateTaskListDto();
            var taskListService = new TaskListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await taskListService.UpdateAsync(updateTaskListDto));
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
            Seed(TripFlipDbContext, TaskListEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = currentUserService;
            var updateTaskListDto = GetUpdateTaskListDto();
            var taskListService = new TaskListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskListService.UpdateAsync(updateTaskListDto), displayName);
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
