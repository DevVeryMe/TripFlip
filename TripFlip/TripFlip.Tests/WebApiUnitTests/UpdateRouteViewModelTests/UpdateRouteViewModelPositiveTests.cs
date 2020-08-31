using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateRouteViewModelTests
{
    public class UpdateRouteViewModelPositiveTests : UpdateRouteViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetValidTitleData), DynamicDataSourceType.Method)]
        public void UpdateRouteViewModel_TitleValidation_Successful(string displayName,
            string validTitle)
        {
            // Arrange.
            var updateRouteViewModel = BuildUpdateRouteViewModel(title: validTitle);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateRouteViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidIdData), DynamicDataSourceType.Method)]
        public void UpdateRouteViewModel_IdValidation_Successful(string displayName,
            int validId)
        {
            // Arrange.
            var updateRouteViewModel = BuildUpdateRouteViewModel(id: validId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateRouteViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidTripIdData), DynamicDataSourceType.Method)]
        public void UpdateRouteViewModel_TripIdValidation_Successful(string displayName,
            int validTripId)
        {
            // Arrange.
            var updateRouteViewModel = BuildUpdateRouteViewModel(tripId: validTripId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateRouteViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetValidTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Build UpdateRouteViewModel object" +
                " and set Title field with MAX allowed valid value.",
                new string('*', 100)
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateRouteViewModel object" +
                " and set Title field with MIN allowed valid value.",
                new string("*")
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateRouteViewModel object" +
                " and set Title field with simple valid value.",
                new string("Default")
            };
        }

        private static IEnumerable<object[]> GetValidIdData()
        {
            yield return new object[]
            {
                "Test case 1: Build UpdateRouteViewModel object" +
                " and set Id field with MAX allowed valid value.",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateRouteViewModel object" +
                " and set Id field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateRouteViewModel object" +
                " and set Id field with simple valid value.",
                100
            };
        }

        private static IEnumerable<object[]> GetValidTripIdData()
        {
            yield return new object[]
            {
                "Test case 1: Build UpdateRouteViewModel object" +
                " and set Trip id field with MAX allowed valid value.",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateRouteViewModel object" +
                " and set Trip id field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateRouteViewModel object" +
                " and set Trip id field with simple valid value.",
                100
            };
        }
    }
}
