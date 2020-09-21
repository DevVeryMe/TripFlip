using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.UserViewModels;

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
            var result = Validator.TryValidateObject(grantRouteRolesViewModel,
                new ValidationContext(grantRouteRolesViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_ChangeUserPasswordViewModel_Given_Not_valid_RouteRoleIds__equals_null_Validation_should_be_failed()
        {
            // Arrange
            var grantRouteRolesViewModel = GetGrantRouteRolesViewModel(routeRoleIds: null);

            // Act
            var result = Validator.TryValidateObject(grantRouteRolesViewModel,
                new ValidationContext(grantRouteRolesViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
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
