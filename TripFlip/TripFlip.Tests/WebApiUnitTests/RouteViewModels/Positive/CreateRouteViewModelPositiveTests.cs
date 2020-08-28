using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TripFlip.ViewModels.RouteViewModels;
using WebApiUnitTests.RouteViewModels.Helpers;

namespace WebApiUnitTests.RouteViewModels.Positive
{
    [TestClass]
    public class CreateRouteViewModelPositiveTests
    {
        #region TestMethods

        [DataTestMethod]
        [DynamicData(nameof(GetCreateRouteViewModelValidData), DynamicDataSourceType.Method)]
        public void CreateRouteViewModel_Validation_Successful(string displayName,
            CreateRouteViewModel createRouteViewModel)
        {
            // Act.
            var modelIsValid = ModelValidator.IsValid(createRouteViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        #endregion

        #region HelpingMethods

        private static IEnumerable<object[]> GetCreateRouteViewModelValidData()
        {
            yield return new object[]
            {
                "Test case 1: Build CreateRouteViewModelBuilder object" +
                " and set fields with MAX allowed valid values.",
                CreateRouteViewModelBuilder.Build(new string('*', 100), int.MaxValue)
            };

            yield return new object[]
            {
                "Test case 2: Build CreateRouteViewModelBuilder object" +
                " and set fields with MIN allowed valid values.",
                CreateRouteViewModelBuilder.Build(new string("*"), 1)
            };

            yield return new object[]
            {
                "Test case 3: Build CreateRouteViewModelBuilder object" +
                " and set fields with simple valid values.",
                CreateRouteViewModelBuilder.Build(new string('*', 50), 50)
            };
        }

        #endregion
    }
}
