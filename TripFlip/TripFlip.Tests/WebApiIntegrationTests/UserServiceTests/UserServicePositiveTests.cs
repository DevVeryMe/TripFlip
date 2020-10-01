using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Helpers;
using WebApiIntegrationTests.CustomComparers;

namespace WebApiIntegrationTests.UserServiceTests
{
    [TestClass]
    public class UserServicePositiveTests : TestUserServiceBase
    {
        private readonly UserDto _expectedRegisteredUser = new UserDto()
        {
            Email = "correct@mail.com"
        };

        private readonly UsersByTripAndCategorizedByRoleDto 
            _expectedUsersByTripAndCategorizedByRoleDto = new UsersByTripAndCategorizedByRoleDto() 
            {
                TripId = 1,
                TripAdmins = new List<UserDto>(),
                TripEditors = new List<UserDto>()
                {
                    new UserDto()
                    {
                        Id = Guid.Parse("c44315ef-547e-4366-888a-46d2e057e6f7"),
                        Email = "seconduser@mail.com"
                    },
                    new UserDto()
                    {
                        Id = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2"),
                        Email = "firstuser@mail.com"
                    }
                },
                TripGuests = new List<UserDto>()
                {
                    new UserDto()
                    {
                        Id = Guid.Parse("816fe98f-515c-407a-bf66-cc9a908644c1"),
                        Email = "thirduser@mail.com"
                    },
                    new UserDto()
                    {
                        Id = Guid.Parse("3ed64e6a-0b5c-423b-a1ec-f0d38c9f6846"),
                        Email = "fourthuser@mail.com"
                    }
                }
            };

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
        public async Task RegisterAsync_GivenValidUserData_Successful()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var registerUserDto = GetRegisterUserDto();
            var comparer = new UserDtoComparer();

            // Act.
            var result = await userService.RegisterAsync(registerUserDto);

            // Assert.
            Assert.AreEqual(0, comparer.Compare(_expectedRegisteredUser, result));
        }

        [TestMethod]
        public async Task ChangePasswordAsync_GivenValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var correctOldPassword = "rel1able-Password";
            var changePasswordDto = GetChangeUserPasswordDto(oldPassword: correctOldPassword);
            var correctNewPassword = "Correctnewpass1@";

            // Act.
            await userService.ChangePasswordAsync(changePasswordDto);
            var user = await TripFlipDbContext.Users.FindAsync(ValidUser.Id);
            var passwordVerified = 
                PasswordHasherHelper.VerifyPassword(correctNewPassword, user.PasswordHash);

            // Assert.
            Assert.IsTrue(passwordVerified);
        }

        [TestMethod]
        public async Task GetAllByTripIdAndCategorizeByRoleAsync_GivenCorrectTripId_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, UserEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            var existentTripId = 1;
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var usersByTripAndCategorizedByRoleDtoComparer = 
                new UsersByTripAndCategorizedByRoleDtoComparer();

            // Act.
            var usersByTripAndCategorizedByRoleDto = 
                await userService.GetAllByTripIdAndCategorizeByRoleAsync(existentTripId);

            // Assert.
            Assert.AreEqual(0, usersByTripAndCategorizedByRoleDtoComparer
                .Compare(_expectedUsersByTripAndCategorizedByRoleDto, 
                    usersByTripAndCategorizedByRoleDto));
        }

        [TestMethod]
        public async Task GrantTripRoleAsync_GivenValidData_Successful()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, UserEntitiesToSeed);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var userToGrantRoles = UserEntitiesToSeed.ToList()[2];

            var grantTripRolesDto = GetGrantTripRolesDto(tripRoleIds: ValidTripRoleIds,
                userId: userToGrantRoles.Id);

            // Act.
            await userService.GrantTripRoleAsync(grantTripRolesDto);

            var usersRolesToCheck = TripFlipDbContext.TripSubscribers
                .Include(subscriber => subscriber.TripRoles)
                .FirstOrDefault(subscriber => subscriber.UserId == userToGrantRoles.Id);

            // Assert.
            Assert.IsNotNull(usersRolesToCheck);
            Assert.AreEqual(3, usersRolesToCheck.TripRoles.Count);

            var validTripRoleIdList = ValidTripRoleIds.ToList();
            var containsAllRoles = usersRolesToCheck.TripRoles
                .Any(role => role.TripRoleId == validTripRoleIdList[0]) && 
                                   usersRolesToCheck.TripRoles
                                       .Any(role => role.TripRoleId == validTripRoleIdList[1]) && 
                                   usersRolesToCheck.TripRoles
                                       .Any(role => role.TripRoleId == validTripRoleIdList[2]);

            Assert.IsTrue(containsAllRoles);
        }

        [TestMethod]
        public async Task SubscribeToRouteAsync_GivenValidData_Successful()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act.
            await userService.SubscribeToRouteAsync(RouteEntityToSeed.Id);

            // Assert.
            var result = TripFlipDbContext.RouteSubscribers
                .FirstOrDefault(subscriber => subscriber.TripSubscriber.UserId == ValidUser.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UnsubscribeFromTripAsync_GivenValidData_Successful()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, NotTripAdminUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(NotTripAdminUser.Id,
                NotTripAdminUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            // Act.
            await userService.UnsubscribeFromTripAsync(TripEntityToSeed.Id);

            // Assert.
            var result = TripFlipDbContext.TripSubscribers
                .FirstOrDefault(subscriber => subscriber.UserId == NotTripAdminUser.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AuthorizeAsync_ValidUserData_Successful()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            Seed(TripFlipDbContext, ValidUser);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var loginDto = GetLoginDto();
            
            // Act.
            var authenticatedUserDto = await userService.AuthorizeAsync(loginDto);

            bool isAuthenticated = loginDto.Email == authenticatedUserDto.Email;

            // Assert.
            Assert.IsTrue(isAuthenticated);
        }

        [TestMethod]
        public async Task GetByIdAsync_ExistentUserId_ExceptionThrown()
        {
            // Arrange.
            var validUser = ValidUser;

            Seed(TripFlipDbContext, validUser);

            var jwtConfiguration = CreateJwtConfiguration();
            var existentUserId = validUser.Id;
            var expectedUserDto = Mapper.Map<UserDto>(validUser);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var userDtoComparer = new UserDtoComparer();

            // Act.
            var resultUserDto = await userService.GetByIdAsync(existentUserId);

            // Assert.
            Assert.AreEqual(0,
                userDtoComparer.Compare(expectedUserDto, resultUserDto));
        }

        [TestMethod]
        public async Task DeleteById_ExistentItemId_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);

            var jwtConfiguration = CreateJwtConfiguration();
            var existentUserId = ValidUser.Id;

            var userService = new UserService(Mapper, TripFlipDbContext,
               jwtConfiguration, CurrentUserService);

            // Act.
            await userService.DeleteByIdAsync(existentUserId);

            // Assert.
            var userEntity = await TripFlipDbContext
                .Users
                .FindAsync(existentUserId);

            Assert.IsNull(userEntity);
        }

        [TestMethod]
        public async Task GrantRouteRoleAsync_GivenValidData_Successful()
        {
            // Arrange.
            var jwtConfiguration = CreateJwtConfiguration();

            var userEntityToGrantRoles = UserEntityToGrantRoles;

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, userEntityToGrantRoles);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRolesEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);

            var grantRouteRolesDto = GetGrantRouteRolesDto(routeRoleIds: ValidRouteRoleIds,
                userId: userEntityToGrantRoles.Id);

            // Act.
            await userService.GrantRouteRoleAsync(grantRouteRolesDto);

            var userRouteRolesToCheck = TripFlipDbContext
                .TripSubscribers
                .Include(s => s.RouteSubscriptions)
                .ThenInclude(s => s.RouteRoles)
                .FirstOrDefault(subscriber => subscriber.UserId == userEntityToGrantRoles.Id)
                .RouteSubscriptions
                .FirstOrDefault(s => s.RouteId == grantRouteRolesDto.RouteId)
                .RouteRoles
                .Select(r => r.RouteRoleId);

            // Assert.
            Assert.AreEqual(ValidRouteRoleIds.Count(), userRouteRolesToCheck.Count());

            bool containsAllRoles = !userRouteRolesToCheck.Except(ValidRouteRoleIds).Any()
                && !ValidRouteRoleIds.Except(userRouteRolesToCheck).Any();

            Assert.IsTrue(containsAllRoles);
        }
    }
}
