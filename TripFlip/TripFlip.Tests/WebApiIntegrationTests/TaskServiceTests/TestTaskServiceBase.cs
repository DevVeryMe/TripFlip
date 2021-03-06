﻿using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Enums;
using TripFlip.Services.Dto.Enums;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;

namespace WebApiIntegrationTests.TaskServiceTests
{
    public class TestTaskServiceBase : TestServiceBase
    {
        protected IEnumerable<int> ValidRouteSubscribersToAssignToTask = new List<int>()
        {
            2
        };

        protected static TripEntity TripEntityToSeed => new TripEntity()
        {
            Id = 1,
            Title = "Trip",
            Description = "Description",
            StartsAt = DateTimeOffset.Parse("28/08/2030 14:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
            EndsAt = DateTimeOffset.Parse("30/11/2030 19:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
        };

        protected RouteEntity RouteEntityToSeed => new RouteEntity()
        {
            Id = 1,
            Title = "Route",
            TripId = 1,
        };

        protected TaskListEntity TaskListEntityToSeed => new TaskListEntity()
        {
            Id = 1,
            RouteId = 1,
            Title = "Task list"
        };

        protected static UserEntity NonExistentUser => new UserEntity()
        {
            Id = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2"),
            Email = "nonexistent@mail.com"
        };

        protected static UserEntity NotTripSubscriberUser => new UserEntity()
        {
            Id = Guid.Parse("c44315ef-547e-4366-888a-46d2e057e6f7"),
            Email = "notsuboftrip@mail.com"
        };

        protected static UserEntity NotRouteSubscriberUser => new UserEntity()
        {
            Id = Guid.Parse("816fe98f-515c-407a-bf66-cc9a908644c1"),
            Email = "notsubofroute@mail.com"
        };

        protected TaskEntity TaskEntityToSeed => new TaskEntity()
        {
            Id = 1,
            Description = "Task",
            IsCompleted = false,
            PriorityLevel = TripFlip.Domain.Entities.Enums.TaskPriorityLevel.Low,
            TaskListId = 1
        };

        protected TaskDto TaskDto => new TaskDto()
        {
            Id = 1,
            Description = "Task",
            IsCompleted = false,
            PriorityLevel = (TripFlip.Services.Dto.Enums.TaskPriorityLevel) 
                TaskPriorityLevel.Low,
            TaskListId = 1
        };

        protected UpdateTaskDto UpdateTaskDto => new UpdateTaskDto()
        {
            Id = 1,
            Description = "Task",
            IsCompleted = false,
            PriorityLevel = (TripFlip.Services.Dto.Enums.TaskPriorityLevel)
                TaskPriorityLevel.Low
        };

        protected UpdateTaskCompletenessDto UpdateTaskCompletenessDto
            => new UpdateTaskCompletenessDto()
            {
                Id = TaskEntityToSeed.Id,
                IsCompleted = true
            };

        protected IEnumerable<TripSubscriberEntity> TripSubscriberEntitiesToSeed =>
            new List<TripSubscriberEntity>()
            {
                new TripSubscriberEntity()
                {
                    Id = 1,
                    TripId = 1,
                    UserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
                },
                new TripSubscriberEntity()
                {
                    Id = 2,
                    TripId = 1,
                    UserId = Guid.Parse("816fe98f-515c-407a-bf66-cc9a908644c1")
                },
                new TripSubscriberEntity()
                {
                    Id = 3,
                    TripId = 1,
                    UserId = Guid.Parse("3ed64e6a-0b5c-423b-a1ec-f0d38c9f6846")
                },
                new TripSubscriberEntity()
                {
                    Id = 4,
                    TripId = 1,
                    UserId = ValidUserWithRouteEditorRole.Id
                }
            };

        protected IEnumerable<RouteSubscriberEntity> RouteSubscriberEntitiesToSeed =>
            new List<RouteSubscriberEntity>()
            {
                new RouteSubscriberEntity()
                {
                    Id = 1,
                    RouteId = 1,
                    TripSubscriberId = 1
                },
                new RouteSubscriberEntity()
                {
                    Id = 2,
                    RouteId = 1,
                    TripSubscriberId = 3
                },
                new RouteSubscriberEntity()
                {
                    Id = 3,
                    RouteId = 1,
                    TripSubscriberId = 4
                }
            };

        protected IEnumerable<RouteRoleEntity> RouteRoleEntitiesToSeed => new List<RouteRoleEntity>()
        {
            new RouteRoleEntity()
            {
                Id = (int)RouteRoles.Admin,
                Name = RouteRoles.Admin.ToString()
            },
            new RouteRoleEntity()
            {
                Id = (int)RouteRoles.Editor,
                Name = RouteRoles.Editor.ToString()
            }
        };

        protected IEnumerable<RouteSubscriberRoleEntity> RouteSubscriberRoleEntitiesToSeed =>
            new List<RouteSubscriberRoleEntity>()
            {
                new RouteSubscriberRoleEntity()
                {
                    RouteRoleId = (int)RouteRoles.Admin,
                    RouteSubscriberId = 1
                },
                new RouteSubscriberRoleEntity()
                {
                    RouteRoleId = (int)RouteRoles.Editor,
                    RouteSubscriberId = 3
                }
            };

        protected RouteSubscriberRoleEntity RouteSubscriberAdminRoleEntityToSeed =>
            new RouteSubscriberRoleEntity()
            {
                RouteRoleId = 1,
                RouteSubscriberId = 1
            };

        protected RouteSubscriberRoleEntity RouteSubscriberEditorRoleEntityToSeed =>
            new RouteSubscriberRoleEntity()
            {
                RouteRoleId = 2,
                RouteSubscriberId = 1
            };

        protected static UserEntity ValidUser => new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "correct@mail.com"
        };

        protected static UserEntity ValidUserWithRouteEditorRole => new UserEntity()
        {
            Id = Guid.Parse("6caedd15-5fbd-4c0a-a5ce-fe363c354123"),
            Email = "correct@mail.com"
        };

        protected static UserEntity RouteSubscriberWithoutRolesUser => new UserEntity()
        {
            Id = Guid.Parse("3ed64e6a-0b5c-423b-a1ec-f0d38c9f6846"),
            Email = "notadminroutrole@mail.com"
        };

        protected PaginationDto PaginationDto => new PaginationDto()
        {
            PageNumber = 1,
            PageSize = 1
        };

        protected PagedList<TaskDto> ExpectedPagedTaskDto => new PagedList<TaskDto>()
        {
            CurrentPage = 1,
            TotalPages = 1,
            PageSize = 1,
            TotalCount = 1,
            Items = new List<TaskDto>()
            {
                TaskDto
            }
        };

        protected ICurrentUserService CurrentUserService;

        protected static ICurrentUserService CreateCurrentUserService(Guid id, string email)
        {
            var correctEmail = email;
            var correctGuid = id;

            var mock = new Mock<ICurrentUserService>();
            mock.Setup(a => a.UserEmail).Returns(correctEmail);
            mock.Setup(a => a.UserId).Returns(correctGuid);

            return mock.Object;
        }

        protected CreateTaskDto GetCreateTaskDto(int taskListId = 1,
            string description = "Description",
            TaskPriorityLevel priorityLevel = TaskPriorityLevel.Low)
        {
            return new CreateTaskDto()
            {
                TaskListId = taskListId,
                Description = description,
                PriorityLevel = priorityLevel
            };
        }

        protected UpdateTaskPriorityDto GetUpdateTaskPriorityDto(int taskId = 1,
            TaskPriorityLevel priorityLevel = TaskPriorityLevel.Low)
        {
            return new UpdateTaskPriorityDto()
            {
                Id = taskId,
                PriorityLevel = priorityLevel
            };
        }

        protected TaskAssigneesDto GetTaskAssigneesDto(IEnumerable<int> routeSubscriberIds,
            int taskId = 1)
        {
            return new TaskAssigneesDto()
            {
                RouteSubscriberIds = routeSubscriberIds,
                TaskId = taskId
            };
        }
    }
}
