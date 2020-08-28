using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.CreateRouteViewModelTests
{
    [TestClass]
    public class CreateRouteViewModelNegativeTests : CreateRouteViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidTitleData), DynamicDataSourceType.Method)]
        public void Title_IsNotValid_ExceptionThrown(string displayName, string notValidTitle)
        {
            // Arrange.
            var createRouteViewModel = BuildCreateRouteViewModel(title: notValidTitle);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(createRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidTripIdData), DynamicDataSourceType.Method)]
        public void TripId_IsNotValid_ExceptionThrown(string displayName, int notValidTripId)
        {
            // Arrange.
            var createRouteViewModel = BuildCreateRouteViewModel(tripId: notValidTripId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(createRouteViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetInvalidTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Test CreateRouteViewModel validation" +
                " if title set to null. Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test CreateRouteViewModel validation" +
                " if title set to empty string. Validation should fail.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Test CreateRouteViewModel validation" +
                " if title length is more than allowed (> 100). Validation should fail.",
                new string('*', 101)
            };
        }

        private static IEnumerable<object[]> GetInvalidTripIdData()
        {
            yield return new object[]
            {
                "Test case 1: Test CreateRouteViewModel validation" +
                " if Trip id is zero. Validation should fail.",
                0
            };

            yield return new object[]
            {
                "Test case 2: Test CreateRouteViewModel validation" +
                " if Trip id is negative number. Validation should fail.",
                -1
            };
        }
    }
}
