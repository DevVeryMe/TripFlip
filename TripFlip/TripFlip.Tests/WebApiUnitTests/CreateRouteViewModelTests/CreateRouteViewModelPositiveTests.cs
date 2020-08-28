using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.CreateRouteViewModelTests
{
    [TestClass]
    public class CreateRouteViewModelPositiveTests : CreateRouteViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetValidTitleData), DynamicDataSourceType.Method)]
        public void CreateRouteViewModel_TitleValidation_Successful(string displayName,
            string validTitle)
        {
            // Arrange.
            var createRouteViewModel = BuildCreateRouteViewModel(title: validTitle);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(createRouteViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidTripIdData), DynamicDataSourceType.Method)]
        public void CreateRouteViewModel_TripIdValidation_Successful(string displayName,
            int validTripId)
        {
            // Arrange.
            var createRouteViewModel = BuildCreateRouteViewModel(tripId: validTripId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(createRouteViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetValidTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Build CreateRouteViewModel object" +
                " and set Title field with MAX allowed valid value.",
                new string('*', 100)
            };

            yield return new object[]
            {
                "Test case 2: Build CreateRouteViewModel object" +
                " and set Title field with MIN allowed valid value.",
                new string("*")
            };

            yield return new object[]
            {
                "Test case 3: Build CreateRouteViewModel object" +
                " and set Title field with simple valid value.",
                new string("Default")
            };
        }

        private static IEnumerable<object[]> GetValidTripIdData()
        {
            yield return new object[]
            {
                "Test case 1: Build CreateRouteViewModel object" +
                " and set Trip id field with MAX allowed valid value.",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 2: Build CreateRouteViewModel object" +
                " and set Trip id field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build CreateRouteViewModel object" +
                " and set Trip id field with simple valid value.",
                100
            };
        }
    }
}
