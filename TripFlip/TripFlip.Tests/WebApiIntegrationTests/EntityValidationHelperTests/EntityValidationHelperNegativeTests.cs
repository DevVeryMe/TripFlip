﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Enums;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.EntityValidationHelperTests
{
    [TestClass]
    public class EntityValidationHelperNegativeTests : TestEntityValidationHelperBase
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
        public async Task ValidateCurrentUserIsSuperAdminAsync_GivenNonExistentCurrentUser_ExceptionThrown()
        {
            // Arrange.
            CurrentUserService = CreateCurrentUserService(NonExistentUser.Id,
                NonExistentUser.Email);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await EntityValidationHelper.ValidateCurrentUserIsSuperAdminAsync(
                    CurrentUserService, TripFlipDbContext));
        }

        [TestMethod]
        public async Task ValidateCurrentUserIsSuperAdminAsync_GivenNotApplicationSuperAdmin_ExceptionThrown()
        {
            // Arrange.
            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            Seed(TripFlipDbContext, ValidUser);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await EntityValidationHelper.ValidateCurrentUserIsSuperAdminAsync(
                    CurrentUserService, TripFlipDbContext));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidData), DynamicDataSourceType.Method)]
        public async Task ValidateCurrentUserTripRoleAsync_GivenNotValidCurrentUser_ExceptionThrown(
           string displayName, ICurrentUserService currentUserService)
        {
            // Arrange
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotTripSubscriberUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);

            CurrentUserService = currentUserService;

            int existingTripId = 1;

            TripRoles validTripRole = TripRoles.Editor;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
                    currentUserService: CurrentUserService,
                    tripFlipDbContext: TripFlipDbContext,
                    tripId: existingTripId,
                    tripRoleToValidate: validTripRole,
                    errorMessage: string.Empty), displayName);
        }

        [TestMethod]
        public async Task ValidateCurrentUserTripRoleAsync_GivenInvalidCurrentUserRole_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRolesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            int existingTripId = 1;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            TripRoles roleThatIsExpectedFromCurrentUser = TripRoles.Editor;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await EntityValidationHelper.ValidateCurrentUserTripRoleAsync(
                    currentUserService: CurrentUserService,
                    tripFlipDbContext: TripFlipDbContext,
                    tripId: existingTripId,
                    tripRoleToValidate: roleThatIsExpectedFromCurrentUser,
                    errorMessage: string.Empty));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetCurrentUserServiceInvalidDataForValidateRouteRole),
            DynamicDataSourceType.Method)]
        public async Task ValidateCurrentUserRouteRoleAsync_GivenNotValidCurrentUser_ExceptionThrown(
           string displayName, ICurrentUserService currentUserService)
        {
            // Arrange.
            Seed(TripFlipDbContext, NonExistentUser);
            Seed(TripFlipDbContext, NotRouteSubscriberUser);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntityToSeed);

            CurrentUserService = currentUserService;

            int existentRouteId = 1;

            RouteRoles validRouteRole = RouteRoles.Editor;

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                    currentUserService: CurrentUserService,
                    tripFlipDbContext: TripFlipDbContext,
                    routeId: existentRouteId,
                    routeRoleToValidate: validRouteRole,
                    errorMessage: string.Empty), displayName);
        }

        [TestMethod]
        public async Task ValidateCurrentUserRouteRoleAsync_GivenInvalidCurrentUserRole_ExceptionThrown()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRolesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntityToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntityToSeed);

            int existingRouteId = 1;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            RouteRoles roleThatIsExpectedFromCurrentUser = RouteRoles.Editor;

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                    currentUserService: CurrentUserService,
                    tripFlipDbContext: TripFlipDbContext,
                    routeId: existingRouteId,
                    routeRoleToValidate: roleThatIsExpectedFromCurrentUser,
                    errorMessage: string.Empty));
        }

        static IEnumerable<object[]> GetCurrentUserServiceInvalidData()
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
        }

        static IEnumerable<object[]> GetCurrentUserServiceInvalidDataForValidateRouteRole()
        {
            yield return new object[]
            {
                "Test case 1: ValidateCurrentUserRouteRoleAsync" +
                "_GivenNotExistentCurrentUser_ExceptionThrown",
                CreateCurrentUserService(NonExistentUser.Id,
                    NonExistentUser.Email)
            };

            yield return new object[]
            {
                "Test case 2: ValidateCurrentUserRouteRoleAsync" +
                "_GivenCurrentUser" +
                "NotSubscribedToRoute_ExceptionThrown",
                CreateCurrentUserService(NotRouteSubscriberUser.Id,
                    NotRouteSubscriberUser.Email)
            };
        }
    }
}
