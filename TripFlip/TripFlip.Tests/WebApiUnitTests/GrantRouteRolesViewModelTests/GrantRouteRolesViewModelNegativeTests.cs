using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.UserViewModels;

namespace WebApiUnitTests.GrantRouteRolesViewModelTests
{
    [TestClass]
    public class GrantRouteRolesViewModelNegativeTests
    {
        [TestMethod]
        public void Create_ChangeUserPasswordViewModel_Given_Not_valid_RouteId_equals_0_Validation_should_be_failed()
        {
            // Arrange
            var updateItemViewModel = GetGrantRouteRolesViewModel(routeId: 0);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        public void Create_ChangeUserPasswordViewModel_Given_Not_valid_RouteRoleIds__equals_nullValidation_should_be_failed()
        {
            // Arrange
            var updateItemViewModel = GetGrantRouteRolesViewModel(routeRoleIds: null);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
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
