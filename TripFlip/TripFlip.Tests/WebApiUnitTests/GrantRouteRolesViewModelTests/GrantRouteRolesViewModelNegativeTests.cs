using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TripFlip.ViewModels.UserViewModels;
using WebApiUnitTests.Helpers;


namespace WebApiUnitTests.GrantRouteRolesViewModelTests
{
    [TestClass]
    public class GrantRouteRolesViewModelNegativeTests
    {
        private static readonly IEnumerable<int> _routeRoleIdsDefaultValue = new List<int>() {1, 2};

        [TestMethod]
        public void Create_ChangeUserPasswordViewModel_Given_Not_valid_RouteId_equals_0_Validation_should_be_failed()
        {
            // Arrange
            var grantRouteRolesViewModel = GetGrantRouteRolesViewModel(routeId: 0,
                routeRoleIds: _routeRoleIdsDefaultValue);

            // Act
            var result = ModelValidator.IsValid(grantRouteRolesViewModel);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestRouteRoleIdsValidation()
        {
            var displayName = "Create_ChangeUserPasswordViewModel_" +
                                 "Given_Not_valid_RouteRoleIds_equals_null_Validation_should_be_failed";

            // Arrange
        var grantRouteRolesViewModel = GetGrantRouteRolesViewModel(routeRoleIds: null);

            // Act
            var result = ModelValidator.IsValid(grantRouteRolesViewModel);

            // Assert
            Assert.IsFalse(result, displayName);
        }

        private static GrantRouteRolesViewModel GetGrantRouteRolesViewModel(IEnumerable<int> routeRoleIds,
            int routeId = 1, 
            Guid userId = default(Guid))
        {
            return new GrantRouteRolesViewModel()
            {
                RouteId = routeId,
                UserId = userId,
                RouteRoleIds = routeRoleIds
            };
        }
    }
}
