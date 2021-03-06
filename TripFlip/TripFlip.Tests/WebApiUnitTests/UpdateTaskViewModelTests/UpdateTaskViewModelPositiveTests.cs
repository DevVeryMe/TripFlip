﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateTaskViewModelTests
{
    public class UpdateTaskViewModelPositiveTests : UpdateTaskViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetValidDescriptionData), DynamicDataSourceType.Method)]
        public void Description_Validation_Successful(string displayName,
            string validDescription)
        {
            // Arrange.
            var updateTaskViewModel = 
                BuildUpdateTaskViewModel(description: validDescription);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateTaskViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidIdData), DynamicDataSourceType.Method)]
        public void Id_Validation_Successful(string displayName, int validId)
        {
            // Arrange.
            var updateTaskViewModel = BuildUpdateTaskViewModel(id: validId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateTaskViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidPriorityLevelData), DynamicDataSourceType.Method)]
        public void PriorityLevel_Validation_Successful(string displayName,
            int validPriorityLevel)
        {
            // Arrange.
            var updateTaskViewModel = 
                BuildUpdateTaskViewModel(priorityLevel: validPriorityLevel);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateTaskViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetValidDescriptionData()
        {
            yield return new object[]
            {
                "Test case 1: Build UpdateTaskViewModel object" +
                " and set Description field with MAX allowed valid value.",
                new string('*', 500)
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateTaskViewModel object" +
                " and set Description field with MIN allowed valid value.",
                new string("*")
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateTaskViewModel object" +
                " and set Description field with simple valid value.",
                new string("Default")
            };
        }

        private static IEnumerable<object[]> GetValidIdData()
        {
            yield return new object[]
            {
                "Test case 1: Build UpdateTaskViewModel object" +
                " and set Id field with MAX allowed valid value.",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateTaskViewModel object" +
                " and set Id field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateTaskViewModel object" +
                " and set Id field with simple valid value.",
                100
            };
        }

        private static IEnumerable<object[]> GetValidPriorityLevelData()
        {
            yield return new object[]
            {
                "Test case 1: Build UpdateTaskViewModel object" +
                " and set Priority level field with MAX allowed valid value.",
                4
            };

            yield return new object[]
            {
                "Test case 2: Build UpdateTaskViewModel object" +
                " and set Priority level field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build UpdateTaskViewModel object" +
                " and set Priority level field with simple valid value.",
                2
            };
        }
    }
}
