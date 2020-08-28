﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TripFlip.ViewModels.RouteViewModels;
using WebApiUnitTests.RouteViewModels.Helpers;

namespace WebApiUnitTests.RouteViewModels.Positive
{
    public class UpdateRouteViewModelPositiveTests
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
                RouteViewModelsBuilder
                .BuildUpdateRouteViewModel(int.MaxValue ,new string('*', 100), int.MaxValue)
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateRouteViewModelBuilder object" +
                " and set fields with MIN allowed valid values.",
                RouteViewModelsBuilder
                .BuildUpdateRouteViewModel(1, new string("*"), 1)
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateRouteViewModelBuilder object" +
                " and set fields with simple valid values.",
                RouteViewModelsBuilder
                .BuildUpdateRouteViewModel(50, new string('*', 50), 50)
            };
        }
    }
}