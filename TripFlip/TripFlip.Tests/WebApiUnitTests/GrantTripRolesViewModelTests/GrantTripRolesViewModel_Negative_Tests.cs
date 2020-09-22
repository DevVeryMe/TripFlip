using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.UserViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.GrantTripRolesViewModelTests
{
    [TestClass]
    public class GrantTripRolesViewModel_Negative_Tests
    {
        readonly IEnumerable<int> _defaultTripRoleIds = new int[] { 1, 2, 3 };

        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_TripId), DynamicDataSourceType.Method)]
        public void Test_GrantTripRoles_TripId_Validation(
            string testCaseDisplayName,
            int invalidTripId)
        {
            // Arrange
            GrantTripRolesViewModel grantTripRolesViewModel =
                Get_GrantTripRolesViewModel(
                    tripId: invalidTripId,
                    tripRoleIds: _defaultTripRoleIds);

            // Act
            bool result = ModelValidator.IsValid(grantTripRolesViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [TestMethod]
        public void Test_GrantTripRoles_TripRoleIds_Validation()
        {
            // Arrange
            string onTestErrorMessage = "Test case of TripRoleIds validation: " +
                "trip roles ids collection was given invalid value that is null. " +
                "Validation should be failed.";
            GrantTripRolesViewModel grantTripRolesViewModel =
                Get_GrantTripRolesViewModel(tripRoleIds: null);

            // Act
            bool result = ModelValidator.IsValid(grantTripRolesViewModel);

            // Assert
            Assert.IsFalse(result, onTestErrorMessage);
        }

        static IEnumerable<object[]> Get_Invalid_TripId()
        {
            yield return new object[]
            {
                "Test case 1 : Test_GrantTripRoles_TripId_Validation was given invalid" +
                " trip id value that is negative integer number (-1)." +
                " Validation should be failed.",
                -1
            };

            yield return new object[]
            {
                "Test case 2 : Test_GrantTripRoles_TripId_Validation was given invalid" +
                " trip id value that equals 0." +
                " Validation should be failed.",
                0
            };
        }

        GrantTripRolesViewModel Get_GrantTripRolesViewModel(
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
