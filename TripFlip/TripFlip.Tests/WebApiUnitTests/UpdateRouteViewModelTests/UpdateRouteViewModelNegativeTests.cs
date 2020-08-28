using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateRouteViewModelTests
{
    [TestClass]
    public class UpdateRouteViewModelNegativeTests : UpdateRouteViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidTitleData), DynamicDataSourceType.Method)]
        public void Title_IsNotValid_ExceptionThrown(string displayName, string notValidTitle)
        {
            // Arrange.
            var updateRouteViewModel = BuildUpdateRouteViewModel(title: notValidTitle);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [TestMethod]
        public void TripId_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            int notValidTripId = -1;

            var updateRouteViewModel = BuildUpdateRouteViewModel(tripId: notValidTripId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        [TestMethod]
        public void Id_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            int notValidId = -1;

            var updateRouteViewModel = BuildUpdateRouteViewModel(id: notValidId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateRouteViewModel);

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
