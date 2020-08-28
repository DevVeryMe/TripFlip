using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateRouteViewModelTests
{
    [TestClass]
    public class UpdateRouteViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidTitleData), DynamicDataSourceType.Method)]
        public void Title_IsEmpty_ExceptionThrown(string displayName, string notValidTitle)
        {
            // Arrange.
            int validId = 1;
            int validTripId = 1;

            var updateRouteViewModel = RouteViewModelsTestsHelper
                .BuildUpdateRouteViewModel(validId, notValidTitle, validTripId);

            // Act.
            bool modelIsValid = RouteViewModelsTestsHelper.IsModelValid(updateRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [TestMethod]
        public void TripId_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            int validId = 1;
            int notValidTripId = -1;
            string validTitle = new string('*', 3);

            var updateRouteViewModel = RouteViewModelsTestsHelper
                .BuildUpdateRouteViewModel(validId, validTitle, notValidTripId);

            // Act.
            bool modelIsValid = RouteViewModelsTestsHelper.IsModelValid(updateRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        [TestMethod]
        public void Id_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            int notValidId = -1;
            int validTripId = 1;
            string validTitle = new string('*', 3);

            var updateRouteViewModel = RouteViewModelsTestsHelper
                .BuildUpdateRouteViewModel(notValidId, validTitle, validTripId);

            // Act.
            bool modelIsValid = RouteViewModelsTestsHelper.IsModelValid(updateRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        private static IEnumerable<object[]> GetInvalidTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Test UpdateRouteViewModelBuilder validation" +
                " if title set to null. Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test UpdateRouteViewModelBuilder validation" +
                " if title set to empty string. Validation should fail.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Test CreateRouteViewModelBuilder validation" +
                " if title length is more than allowed (> 100). Validation should fail.",
                new string('*', 101)
            };
        }
    }
}
