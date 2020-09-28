using System;
using System.Collections.Generic;
using System.Globalization;
using Moq;
using TripFlip.Domain.Entities;
using TripFlip.Services.Configurations;
using TripFlip.Services.Dto.Enums;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.UserServiceTests
{
    public class TestUserServiceBase : TestServiceBase
    {
        protected IEnumerable<int> ValidTripRoleIds = new []{1, 2, 3};

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

        protected UserEntity ExistentButNotSubscribedToTripUser = new UserEntity()
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
            TripId = 1,
            Title = "Route"
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
    }
}
