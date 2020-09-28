using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.TaskServiceTests
{
    [TestClass]
    public class TaskServiceNegativeTests : TestTaskServiceBase
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
            // Arrange.
            var invalidId = 2;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.GetByIdAsync(invalidId));
        }

        [TestMethod]
        public async Task CreateAsync_GivenNonExistentTaskListId_ExceptionThrown()
        {
            // Arrange.
            var createTaskDto = GetCreateTaskDto();
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.CreateAsync(createTaskDto));
        }

        [TestMethod]
        public async Task CreateAsync_GivenCurrentUserNotRouteAdmin_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var createTaskDto = GetCreateTaskDto();
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await taskService.CreateAsync(createTaskDto));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_GivenNotValidId_ExceptionThrown()
        {
            // Arrange
            var invalidId = 2;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.DeleteByIdAsync(invalidId));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_GivenCurrentUserNotRouteAdmin_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, RouteSubscriberWithoutRolesUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(RouteSubscriberWithoutRolesUser.Id,
                RouteSubscriberWithoutRolesUser.Email);
            var validTaskId = 1;
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await taskService.DeleteByIdAsync(validTaskId));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task CreateAsync_GivenNotValidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            var createTaskDto = GetCreateTaskDto();
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.CreateAsync(createTaskDto), displayName);
        }

        public async Task DeleteByIdAsync_GivenNotValidCurrentUser_ExceptionThrown(
        string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

            CurrentUserService = currentUserService;
            var validTaskId = 1;
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.DeleteByIdAsync(validTaskId));
        }

        [TestMethod]
        public async Task UpdatePriorityAsync_NonExistentTask_ExceptionThrown()
        {
            // Arrange.
            var updateTaskPriorityDto = GetUpdateTaskPriorityDto();
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.UpdatePriorityAsync(updateTaskPriorityDto));
        }

        [TestMethod]
        public async Task UpdatePriorityAsync_CurrentUserNotRouteEditor_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, RouteSubscriberWithoutRolesUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(RouteSubscriberWithoutRolesUser.Id,
                RouteSubscriberWithoutRolesUser.Email);

            var updateTaskPriorityDto = GetUpdateTaskPriorityDto();
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await taskService.UpdatePriorityAsync(updateTaskPriorityDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task UpdatePriorityAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            var updateTaskPriorityDto = GetUpdateTaskPriorityDto();
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.UpdatePriorityAsync(updateTaskPriorityDto), displayName);
        }

        [TestMethod]
        public async Task SetTaskAssigneesAsync_GivenNonExistentTask_ExceptionThrown()
        {
            // Arrange.
            var taskAssigneesDto = GetTaskAssigneesDto(routeSubscriberIds:
                ValidRouteSubscribersToAssignToTask);
            var taskService = new TaskService(TripFlipDbContext, Mapper, null);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.SetTaskAssignees(taskAssigneesDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task SetTaskAssigneesAsync_InvalidCurrentUser_ExceptionThrown(
            string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            var taskAssigneesDto = GetTaskAssigneesDto(routeSubscriberIds:
                ValidRouteSubscribersToAssignToTask);
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.SetTaskAssignees(taskAssigneesDto), displayName);
        }

        [TestMethod]
        public async Task SetTaskAssigneesAsync_CurrentUserNotRouteEditor_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, RouteSubscriberWithoutRolesUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);

            var notRouteEditorUserId = RouteSubscriberWithoutRolesUser.Id;
            var notRouteEditorUserEmail = RouteSubscriberWithoutRolesUser.Email;

            CurrentUserService = CreateCurrentUserService(notRouteEditorUserId,
                notRouteEditorUserEmail);

            var taskAssigneesDto = GetTaskAssigneesDto(routeSubscriberIds:
                ValidRouteSubscribersToAssignToTask);
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await taskService.SetTaskAssignees(taskAssigneesDto));
        }

        [TestMethod]
        public async Task SetTaskAssigneesAsync_GivenNonExistentRouteSubscriberIds_ExceptionThrown()
        {
            // Arrange.
            var invalidRouteSubscriberIds = new List<int>()
            {
                10, 100, 1000
            };

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEditorRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var taskAssigneesDto = GetTaskAssigneesDto(routeSubscriberIds:
                invalidRouteSubscriberIds);
            var taskService = new TaskService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await taskService.SetTaskAssignees(taskAssigneesDto));
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
