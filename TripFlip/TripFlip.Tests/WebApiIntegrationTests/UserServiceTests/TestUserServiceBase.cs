using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Moq;
using TripFlip.Domain.Entities;
using TripFlip.Services.Configurations;
using TripFlip.Services.Dto.Enums;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.RoutePointDtos;
using TripFlip.Services.Dto.RouteSubscriberDtos;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Dto.TripRoleDtos;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;

namespace WebApiIntegrationTests.UserServiceTests
{
    public class TestUserServiceBase : TestServiceBase
    {
        protected IEnumerable<int> ValidTripRoleIds = new []{1, 2, 3};

        protected IEnumerable<int> ValidRouteRoleIds = new[] { 1, 2 };

        protected IEnumerable<int> ValidApplicationRoleIds = new int[]
        {
            (int) ApplicationRole.SuperAdmin,
            (int) ApplicationRole.Admin
        };

        protected UserEntity ValidUser => new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "correct@mail.com",
            PasswordHash = "AQAAAAEAACcQAAAAEC5xRaJ3jHVV9NthGohTbm" +
                           "0wURGSB7OqYnLQkM42Y7ydc452Fqav1EnS5u7+MVdxsA=="
        };

        protected UserEntity InvalidUser => new UserEntity()
        {
            Id = Guid.Parse("d03bba28-88ac-452b-a3e0-19fa2921a99d"),
            Email = "incorrect@mail.com"
        };

        protected UserEntity NotTripAdminUser => new UserEntity()
        {
            Id = Guid.Parse("1a5104ab-b479-4547-9c00-7dd930b31267"),
            Email = "nottripadmin@mail.com"
        };

        protected static UserEntity NonExistentUser => new UserEntity()
        {
            Id = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2"),
            Email = "nonexistent@mail.com"
        };

        protected static UserEntity UserEntityToGrantRoles => new UserEntity()
        {
            Id = Guid.Parse("c44315ef-547e-4366-888a-46d2e057e6f7"),
            Email = "notsuboftrip@mail.com"
        };

        protected UserEntity NotTripSubscriberUser = new UserEntity()
        {
            Id = Guid.Parse("7d6ac0ab-575f-444b-a813-d56f9d6ba0e5"),
            Email = "nottripsubscribed@mail.com"
        };

        protected TripEntity TripEntityToSeed => new TripEntity()
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
            TripId = TripEntityToSeed.Id,
            Title = "Route"
        };

        protected RoutePointEntity RoutePointEntityToSeed => new RoutePointEntity()
        {
            Id = 1,
            Longitude = default,
            Latitude = default,
            Order = default,
            DateCreated = DateTimeOffset.Parse("28/08/2010 14:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
            RouteId = RouteEntityToSeed.Id
        };

        protected ItemListEntity ItemListEntityToSeed => new ItemListEntity()
        {
            Id = 1,
            Title = "ItemList",
            RouteId = RouteEntityToSeed.Id
        };

        protected ItemEntity ItemEntityToSeed => new ItemEntity()
        {
            Id = 1,
            Title = "Item",
            Comment = "Comment",
            Quantity = "Quantity",
            IsCompleted = false,
            ItemListId = ItemListEntityToSeed.Id,
        };

        protected ItemAssigneeEntity ItemAssigneeEntityToSeed => new ItemAssigneeEntity()
        {
            ItemId = ItemEntityToSeed.Id,
            RouteSubscriberId = RouteSubscriberEntityToSeed.Id
        };

        protected TaskListEntity TaskListEntityToSeed => new TaskListEntity()
        {
            Id = 1,
            RouteId = RouteEntityToSeed.Id,
            Title = "Task list"
        };

        protected TaskEntity TaskEntityToSeed => new TaskEntity()
        {
            Id = 1,
            Description = "Task",
            IsCompleted = false,
            PriorityLevel = TripFlip.Domain.Entities.Enums.TaskPriorityLevel.Low,
            TaskListId = TaskListEntityToSeed.Id
        };

        protected TaskAssigneeEntity TaskAssigneeEntityToSeed => new TaskAssigneeEntity()
        {
            TaskId = TaskEntityToSeed.Id,
            RouteSubscriberId = RouteSubscriberEntityToSeed.Id
        };

        protected IEnumerable<TripSubscriberEntity> TripSubscriberEntitiesToSeed =>
            new List<TripSubscriberEntity>()
            {
                new TripSubscriberEntity()
                {
                    Id = 1,
                    TripId = 1,
                    UserId = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2")
                },
                new TripSubscriberEntity()
                {
                    Id = 2,
                    TripId = 1,
                    UserId = Guid.Parse("c44315ef-547e-4366-888a-46d2e057e6f7")
                },
                new TripSubscriberEntity()
                {
                    Id = 3,
                    TripId = 1,
                    UserId = Guid.Parse("816fe98f-515c-407a-bf66-cc9a908644c1")
                },
                new TripSubscriberEntity()
                {
                    Id = 4,
                    TripId = 1,
                    UserId = Guid.Parse("3ed64e6a-0b5c-423b-a1ec-f0d38c9f6846")
                },
                new TripSubscriberEntity()
                {
                    Id = 5,
                    TripId = 1,
                    UserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
                },
                new TripSubscriberEntity()
                {
                    Id = 6,
                    TripId = 1,
                    UserId = Guid.Parse("1a5104ab-b479-4547-9c00-7dd930b31267")
                }
            };

        protected RouteSubscriberEntity RouteSubscriberEntityToSeed =>
            new RouteSubscriberEntity()
            {
                Id = 1,
                RouteId = 1,
                TripSubscriberId = 5
            };

        protected IEnumerable<UserEntity> UserEntitiesToSeed =>
            new List<UserEntity>()
            {
                new UserEntity()
                {
                    Id = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2"),
                    Email = "firstuser@mail.com"
                },
                new UserEntity()
                {
                    Id = Guid.Parse("c44315ef-547e-4366-888a-46d2e057e6f7"),
                    Email = "seconduser@mail.com"
                },
                new UserEntity()
                {
                    Id = Guid.Parse("816fe98f-515c-407a-bf66-cc9a908644c1"),
                    Email = "thirduser@mail.com"
                },
                new UserEntity()
                {
                    Id = Guid.Parse("3ed64e6a-0b5c-423b-a1ec-f0d38c9f6846"),
                    Email = "fourthuser@mail.com"
                }
            };

        protected IEnumerable<TripRoleEntity> TripRolesEntitiesToSeed =>
            new List<TripRoleEntity>()
            {
                new TripRoleEntity()
                {
                    Id = 1,
                    Name = "Admin"
                },
                new TripRoleEntity()
                {
                    Id = 2,
                    Name = "Editor"
                },
                new TripRoleEntity()
                {
                    Id = 3,
                    Name = "Guest"
                }
            };

        protected IEnumerable<RouteRoleEntity> RouteRolesEntitiesToSeed =>
            new List<RouteRoleEntity>()
            {
                new RouteRoleEntity()
                {
                    Id = 1,
                    Name = "Admin"
                },
                new RouteRoleEntity()
                {
                    Id = 2,
                    Name = "Editor"
                }
            };

        protected IEnumerable<TripSubscriberRoleEntity> TripSubscriberRoleEntitiesToSeed =>
            new List<TripSubscriberRoleEntity>()
            {
                new TripSubscriberRoleEntity()
                {
                    TripRoleId = 2,
                    TripSubscriberId = 1
                },
                new TripSubscriberRoleEntity()
                {
                    TripRoleId = 2,
                    TripSubscriberId = 2
                },
                new TripSubscriberRoleEntity()
                {
                    TripRoleId = 3,
                    TripSubscriberId = 3
                },
                new TripSubscriberRoleEntity()
                {
                    TripRoleId = 3,
                    TripSubscriberId = 4
                },
                new TripSubscriberRoleEntity()
                {
                    TripRoleId = 1,
                    TripSubscriberId = 5
                },
            };

        protected IEnumerable<ApplicationRoleEntity> ApplicationRoleEntitiesToSeed =>
            new List<ApplicationRoleEntity>()
            {
                new ApplicationRoleEntity()
                {
                    Id = (int) ApplicationRole.SuperAdmin,
                    Name = ApplicationRole.SuperAdmin.ToString()
                },
                new ApplicationRoleEntity()
                {
                    Id = (int) ApplicationRole.Admin,
                    Name = ApplicationRole.Admin.ToString()
                }
            };

        protected ApplicationUserRoleEntity ApplicationSuperAdminUserRoleToSeed =>
            new ApplicationUserRoleEntity()
            {
                UserId = ValidUser.Id,
                ApplicationRoleId = (int) ApplicationRole.SuperAdmin
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

        protected static JwtConfiguration CreateJwtConfiguration()
        {
            var jwtConfiguration = new JwtConfiguration()
            {
                SecretKey = "TripFlip-WebApi-SecretKey",
                Issuer = "TripFlip-WebApi",
                Audience = "TripFlip-WebApi-User",
                TokenLifetime = 10
            };

            return jwtConfiguration;
        }

        protected RegisterUserDto GetRegisterUserDto(string email = "correct@mail.com",
            string password = "Correctpass1@", string aboutMe = null, string firstName = null,
            string lastName = null, DateTimeOffset? birthDate = null, UserGender? gender = null)
        {
            return new RegisterUserDto()
            {
                Email = email,
                Password = password,
                AboutMe = aboutMe,
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                Gender = gender
            };
        }

        protected ChangeUserPasswordDto GetChangeUserPasswordDto(
            string oldPassword = "Correctpass1@", string newPassword = "Correctnewpass1@")
        {
            return new ChangeUserPasswordDto()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword
            };
        }

        protected GrantTripRolesDto GetGrantTripRolesDto(IEnumerable<int> tripRoleIds, 
            Guid userId, int tripId = 1)
        {
            return new GrantTripRolesDto()
            {
                TripId = tripId,
                TripRoleIds = tripRoleIds,
                UserId = userId
            };
        }

        protected LoginDto GetLoginDto(string email = "correct@mail.com",
            string password = "rel1able-Password")
        {
            return new LoginDto()
            {
                Email = email,
                Password = password
            };
        }

        protected GrantRouteRolesDto GetGrantRouteRolesDto(
            Guid userId,
            IEnumerable<int> routeRoleIds,
            int routeId = 1)
        {
            return new GrantRouteRolesDto()
            {
                UserId = userId,
                RouteRoleIds = routeRoleIds,
                RouteId = routeId
            };
        }

        protected GrantApplicationRolesDto Get_GrantApplicationRolesDto(
            Guid userId,
            IEnumerable<int> applicationRoleIds)
        {
            return new GrantApplicationRolesDto()
            {
                UserId = userId,
                ApplicationRoleIds = applicationRoleIds
            };
        }

        protected UpdateUserProfileDto Get_UpdateUserProfileDto()
        {
            UserEntity validUser = ValidUser;

            return new UpdateUserProfileDto()
            {
                Email = validUser.Email,
                FirstName = validUser.FirstName,
                LastName = validUser.LastName,
                AboutMe = validUser.AboutMe,
                Gender = (UserGender?)validUser.Gender,
                BirthDate = validUser.BirthDate
            };
        }

        protected PagedList<UserDto> Get_Expected_PagedUserDtos()
        {
            var userDtos = Mapper.Map< IEnumerable<UserDto> >(UserEntitiesToSeed);

            int userDtosLength = userDtos.Count();

            return new PagedList<UserDto>(
                pageNumber: 1,
                pageSize: userDtosLength,
                totalCount: userDtosLength,
                items: userDtos);
        }

        protected IEnumerable<TripWithRoutesAndUserRolesDto> 
            Get_Expected_TripWithRoutesAndUserRolesDto()
        {
            var tripEntity = TripEntityToSeed;
            var routeEntity = RouteEntityToSeed;
            var itemListEntity = ItemListEntityToSeed;
            var itemEntity = ItemEntityToSeed;
            var taskListEntity = TaskListEntityToSeed;
            var taskEntity = TaskEntityToSeed;
            var routeSubscriberEntity = RouteSubscriberEntityToSeed;
            var routePointEntity = RoutePointEntityToSeed;

            return new List<TripWithRoutesAndUserRolesDto>
            {
                new TripWithRoutesAndUserRolesDto()
                {
                    Id = tripEntity.Id,
                    Title = tripEntity.Title,
                    Description = tripEntity.Description,
                    StartsAt = tripEntity.StartsAt,
                    EndsAt = tripEntity.EndsAt,

                    Routes = new List<RouteWithPointsItemAndTaskListsDto>
                    {
                        new RouteWithPointsItemAndTaskListsDto
                        {
                            Id = routeEntity.Id,
                            Title = routeEntity.Title,
                            ItemLists = new List<ItemListWithItemsDto>
                            {
                                new ItemListWithItemsDto
                                {
                                    Id = itemListEntity.Id,
                                    Title = itemListEntity.Title,
                                    Items = new List<ItemWithAssigneesDto>
                                    {
                                        new ItemWithAssigneesDto
                                        {
                                            Id = itemEntity.Id,
                                            Title = itemEntity.Title,
                                            Comment = itemEntity.Comment,
                                            Quantity = itemEntity.Quantity,
                                            IsCompleted = itemEntity.IsCompleted,
                                            ItemAssignees = new List<RouteSubscriberDto>
                                            {
                                                new RouteSubscriberDto
                                                {
                                                    Id = routeSubscriberEntity.Id,
                                                    TripSubscriberId = routeSubscriberEntity.TripSubscriberId
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            TaskLists = new List<TaskListWithTasksDto>
                            {
                                new TaskListWithTasksDto
                                {
                                    Id = taskListEntity.Id,
                                    Title = taskListEntity.Title,
                                    Tasks = new List<TaskWithAssigneesDto>
                                    {
                                        new TaskWithAssigneesDto
                                        {
                                            Id = taskEntity.Id,
                                            Description = taskEntity.Description,
                                            PriorityLevel = (TaskPriorityLevel)taskEntity.PriorityLevel,
                                            DateCreated = taskEntity.DateCreated,
                                            IsCompleted = taskEntity.IsCompleted,
                                            TaskAssignees = new List<RouteSubscriberDto>
                                            {
                                                new RouteSubscriberDto
                                                {
                                                    Id = routeSubscriberEntity.Id,
                                                    TripSubscriberId = routeSubscriberEntity.TripSubscriberId
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            RoutePoints = new List<RoutePointDto>
                            {
                                new RoutePointDto
                                {
                                    Id = routePointEntity.Id,
                                    Latitude = routePointEntity.Latitude,
                                    Longitude = routePointEntity.Longitude,
                                    Order = routePointEntity.Order
                                }
                            }
                        }
                    },
                    TripRoles = new List<TripRoleDto>{}
                }
            };
        }
    }
}
