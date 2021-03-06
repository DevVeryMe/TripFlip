﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateTaskCompletenessViewModelTests
{
    [TestClass]
    public class UpdateTaskCompletenessViewModelPositiveTests
        : UpdateTaskCompletenessViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetValidIdData), DynamicDataSourceType.Method)]
        public void Id_Validation_Successful(string displayName, int validId)
        {
            // Arrange.
            var updateTaskCompletenessViewModel = 
                BuildUpdateTaskCompletenessViewModel(id: validId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateTaskCompletenessViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetValidIdData()
        {
            yield return new object[]
            {
                "Test case 1: Build UpdateTaskCompletenessViewModel object" +
                " and set Id field with MAX allowed valid value.",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateTaskCompletenessViewModel object" +
                " and set Id field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateTaskCompletenessViewModel object" +
                " and set Id field with simple valid value.",
                100
            };
        }
    }
}
