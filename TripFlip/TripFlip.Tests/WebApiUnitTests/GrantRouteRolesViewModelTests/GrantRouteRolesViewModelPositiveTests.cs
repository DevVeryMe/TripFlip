﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.UserViewModels;

namespace WebApiUnitTests.GrantRouteRolesViewModelTests
{
    public class GrantRouteRolesViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestRouteIdData), DynamicDataSourceType.Method)]
        public void TestGrantRouteRolesRouteIdValidation(string displayName,
            int routeId)
        {
            // Arrange
            var updateItemViewModel = GetGrantRouteRolesViewModel(routeId: routeId);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestUserIdData), DynamicDataSourceType.Method)]
        public void TestGrantRouteRolesUserIdValidation(string displayName,
            Guid userId)
        {
            // Arrange
            var updateItemViewModel = GetGrantRouteRolesViewModel(userId: userId);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestUserIdData), DynamicDataSourceType.Method)]
        public void TestGrantRouteRolesRouteRoleIdsValidation(string displayName,
            IEnumerable<int> routeRoleIds)
        {
            // Arrange
            var updateItemViewModel = GetGrantRouteRolesViewModel(routeRoleIds: routeRoleIds);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetTestRouteIdData()
        {
            yield return new object[]
            {
                "Test case 1: Create_GrantRouteRolesViewModel_Given_Valid_RouteId_min_value_" +
                "Validation_should_be_successful",
                1
            };

            yield return new object[]
            {
                "Test case 2: Create_GrantRouteRolesViewModel_Given_Valid_Id_max_value_" +
                "possible_Validation_should_be_successful",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 3: Create_GrantRouteRolesViewModel_Given_Valid_Id_average_value_" +
                "Validation_should_be_successful",
                2500
            };
        }

        private static IEnumerable<object[]> GetTestUserIdData()
        {
            yield return new object[]
            {
                "Test case 1: Create_GrantRouteRolesViewModel_Given_Valid_UserId_min_value_" +
                "Validation_should_be_successful",
                Guid.Empty
            };

            yield return new object[]
            {
                "Test case 2: Create_GrantRouteRolesViewModel_Given_Valid_UserId_average_value_" +
                "_Validation_should_be_successful",
                int.MaxValue
            };
        }

        private static IEnumerable<object[]> GetTestRouteRoleIdsData()
        {
            yield return new object[]
            {
                "Test case 1: Create_GrantRouteRolesViewModel_Given_Valid_RouteRoleIds_min_value_" +
                "Validation_should_be_successful",
                Enumerable.Empty<int>()
            };

            yield return new object[]
            {
                "Test case 2: Create_GrantRouteRolesViewModel_Given_Valid_RouteRoleIds_average_value_" +
                "_Validation_should_be_successful",
                new List<int>(){1, 2, 3} 
            };
        }

        private static GrantRouteRolesViewModel GetGrantRouteRolesViewModel(int routeId = 1,
            Guid userId = default(Guid),
            IEnumerable<int> routeRoleIds = null)
        {
            routeRoleIds ??= new List<int>()
            {
                1, 2
            };

            return new GrantRouteRolesViewModel()
            {
                RouteId = routeId,
                UserId = userId,
                RouteRoleIds = routeRoleIds
            };
        }
    }
}
