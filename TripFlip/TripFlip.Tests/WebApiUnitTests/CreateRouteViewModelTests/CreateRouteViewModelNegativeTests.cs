using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.CreateRouteViewModelTests
{
    [TestClass]
    public class CreateRouteViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidTitleData), DynamicDataSourceType.Method)]
        public void Title_IsEmpty_ExceptionThrown(string displayName, string notValidTitle)
        {
            // Arrange.
            int validTripId = 1;

            var createRouteViewModel = RouteViewModelsTestsHelper
                .BuildCreateRouteViewModel(notValidTitle, validTripId);

            // Act.
            bool modelIsValid = RouteViewModelsTestsHelper.IsModelValid(createRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [TestMethod]
        public void TripId_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            string validTitle = new string('*', 3);
            int notValidTripId = -1;

            var createRouteViewModel = RouteViewModelsTestsHelper
                .BuildCreateRouteViewModel(validTitle, notValidTripId);

            // Act.
            bool modelIsValid = RouteViewModelsTestsHelper.IsModelValid(createRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        private static IEnumerable<object[]> GetInvalidTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Test CreateRouteViewModelBuilder validation" +
                " if title set to null. Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test CreateRouteViewModelBuilder validation" +
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
