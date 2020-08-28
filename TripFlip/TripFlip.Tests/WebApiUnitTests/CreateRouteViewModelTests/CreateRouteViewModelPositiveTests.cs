﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TripFlip.ViewModels.RouteViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.CreateRouteViewModelTests
{
    [TestClass]
    public class CreateRouteViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetCreateRouteViewModelValidData), DynamicDataSourceType.Method)]
        public void CreateRouteViewModel_Validation_Successful(string displayName,
            CreateRouteViewModel createRouteViewModel)
        {
            // Act.
            bool modelIsValid = RouteViewModelsTestsHelper.IsModelValid(createRouteViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetCreateRouteViewModelValidData()
        {
            yield return new object[]
            {
                "Test case 1: Build CreateRouteViewModelBuilder object" +
                " and set fields with MAX allowed valid values.",
                RouteViewModelsTestsHelper
                .BuildCreateRouteViewModel(title: new string('*', 100), tripId: int.MaxValue)
            };

            yield return new object[]
            {
                "Test case 2: Build CreateRouteViewModelBuilder object" +
                " and set fields with MIN allowed valid values.",
                RouteViewModelsTestsHelper
                .BuildCreateRouteViewModel(title: new string("*"), tripId: 1)
            };

            yield return new object[]
            {
                "Test case 3: Build CreateRouteViewModelBuilder object" +
                " and set fields with simple valid values.",
                RouteViewModelsTestsHelper
                .BuildCreateRouteViewModel(title: new string('*', 50), tripId: 50)
            };
        }
    }
}
