using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.UserViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.GrantTripRolesViewModelTests
{
    [TestClass]
    public class GrantTripRolesViewModel_Positive_Tests
    {
        readonly IEnumerable<int> _defaultTripRoleIds = new int[] { 1, 2, 3 };

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_TripId), DynamicDataSourceType.Method)]
        public void Test_GrantTripRoles_TripId_Validation(
            string testCaseDisplayName,
            int validTripId)
        {
            // Arrange
            GrantTripRolesViewModel grantTripRolesViewModel =
                Get_Valid_GrantTripRolesViewModel(
                    tripId: validTripId,
                    tripRoleIds: _defaultTripRoleIds);

            // Act
            bool result = ModelValidator.IsValid(grantTripRolesViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_UserId), DynamicDataSourceType.Method)]
        public void Test_GrantTripRoles_UserId_Validation(
            string testCaseDisplayName,
            Guid validUserId)
        {
            // Arrange
            GrantTripRolesViewModel grantTripRolesViewModel =
                Get_Valid_GrantTripRolesViewModel(
                    userId: validUserId,
                    tripRoleIds: _defaultTripRoleIds);

            // Act
            bool result = ModelValidator.IsValid(grantTripRolesViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_RouteSubscriberIds), DynamicDataSourceType.Method)]
        public void Test_GrantTripRoles_TripRoleIds_Validation(
            string testCaseDisplayName,
            IEnumerable<int> validTripRoleIds)
        {
            // Arrange
            GrantTripRolesViewModel grantTripRolesViewModel =
                Get_Valid_GrantTripRolesViewModel(tripRoleIds: validTripRoleIds);

            // Act
            bool result = ModelValidator.IsValid(grantTripRolesViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Valid_TripId()
        {
            yield return new object[]
            {
                "Test case 1 : Test_GrantTripRoles_TripId_Validation was given Id with minimal" +
                " valid value that equals 1. Validation should be successful.",
                1
            };

            yield return new object[]
            {
                "Test case 2 : Test_GrantTripRoles_TripId_Validation was given Id with maximum" +
                " valid length that equals maximum number that is supported by integer." +
                " Validation should be successful.",
                int.MaxValue
            };
        }

        static IEnumerable<object[]> Get_Valid_UserId()
        {
            yield return new object[]
            {
                "Test case 1: Test_GrantTripRoles_UserId_Validation was given valid " +
                "user Id with empty Guid value. Validation should be successful..",
                Guid.Empty
            };

            yield return new object[]
            {
                "Test case 2: Test_GrantTripRoles_UserId_Validation was given valid " +
                "user Id with valid Guid value. Validation should be successful.",
                Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
            };
        }

        static IEnumerable<object[]> Get_Valid_RouteSubscriberIds()
        {
            yield return new object[]
            {
                "Test case 1 : Test_GrantTripRoles_TripRoleIds_Validation " +
                "was given valid trip role ids collection that is empty. " +
                "Validation should be successful.",
                new int[] { }
            };

            yield return new object[]
            {
                "Test case 2 : Test_GrantTripRoles_TripRoleIds_Validation " +
                "was given valid trip role ids collection with different values. " +
                "Validation should be successful.",
                new int[] {1, 2, int.MaxValue}
            };
        }

        GrantTripRolesViewModel Get_Valid_GrantTripRolesViewModel(
            int tripId = 1,
            Guid userId = default,
            IEnumerable<int> tripRoleIds = default)
        {
            return new GrantTripRolesViewModel()
            {
                TripId = tripId,
                UserId = userId,
                TripRoleIds = tripRoleIds
            };
        }
    }
}
