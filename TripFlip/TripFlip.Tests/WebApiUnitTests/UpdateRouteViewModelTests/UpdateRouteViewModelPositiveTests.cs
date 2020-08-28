using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TripFlip.ViewModels.RouteViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateRouteViewModelTests
{
    public class UpdateRouteViewModelPositiveTests : UpdateRouteViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetUpdateRouteViewModelValidData), DynamicDataSourceType.Method)]
        public void UpdateRouteViewModel_Validation_Successful(string displayName,
            UpdateRouteViewModel updateRouteViewModel)
        {
            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateRouteViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetUpdateRouteViewModelValidData()
        {
            yield return new object[]
            {
                "Test case 1: Build UpdateRouteViewModelBuilder object" +
                " and set fields with MAX allowed valid values.",
                BuildUpdateRouteViewModel(id: int.MaxValue, title: new string('*', 100), tripId: int.MaxValue)
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateRouteViewModelBuilder object" +
                " and set fields with MIN allowed valid values.",
                BuildUpdateRouteViewModel(id: 1, title: new string("*"), tripId: 1)
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateRouteViewModelBuilder object" +
                " and set fields with simple valid values.",
                BuildUpdateRouteViewModel(id: 50, title: new string('*', 50), tripId: 50)
            };
        }
    }
}
