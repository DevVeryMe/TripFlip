using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.GrantApplicationRolesViewModelTests
{
    [TestClass]
    public class GrantApplicationRolesViewModelPositiveTests
        : GrantApplicationRolesViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestUserIdData), DynamicDataSourceType.Method)]
        public void UserId_Validation_Successful(string displayName,
            Guid userId)
        {
            // Arrange
            var validApplicationRoleIds = new int[] { };

            var grantApplicationRolesViewModel = 
                BuildGrantApplicationRolesViewModel(validApplicationRoleIds, userId);

            // Act
            bool modelIsValid = ModelValidator.IsValid(grantApplicationRolesViewModel);

            // Assert
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestApplicationRoleIdsData), DynamicDataSourceType.Method)]
        public void ApplicationRoleIds_Validation_Successful(string displayName,
            IEnumerable<int> validApplicationRoleIds)
        {
            // Arrange
            var grantApplicationRolesViewModel = 
                BuildGrantApplicationRolesViewModel(validApplicationRoleIds);

            // Act
            bool midelIsValid = ModelValidator.IsValid(grantApplicationRolesViewModel);

            // Assert
            Assert.IsTrue(midelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetTestUserIdData()
        {
            yield return new object[]
            {
                "Test case 1: Build GrantApplicationRolesViewModel object" +
                " and set UserId field with MIN allowed valid value." +
                " Validation successful.",
                Guid.Empty
            };

            yield return new object[]
            {
                "Test case 2: Build GrantApplicationRolesViewModel object" +
                " and set UserId field with average valid value." +
                " Validation successful.",
                Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
            };
        }

        private static IEnumerable<object[]> GetTestApplicationRoleIdsData()
        {
            yield return new object[]
            {
                "Test case 1: Build GrantApplicationRolesViewModel object" +
                " and make ApplicationRoleIds array is empty." +
                " Validation successful.",
                new int[] { }
        };

            yield return new object[]
            {
                "Test case 2: Build GrantApplicationRolesViewModel object" +
                " and set ApplicationRoleIds array with average valid values." +
                " Validation successful.",
                new int[] { 1, 2, 3 }
            };
        }
    }
}
