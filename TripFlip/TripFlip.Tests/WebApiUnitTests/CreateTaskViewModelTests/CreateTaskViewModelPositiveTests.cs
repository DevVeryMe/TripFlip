using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.CreateTaskViewModelTests
{
    public class CreateTaskViewModelPositiveTests : CreateTaskViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetValidDescriptionData), DynamicDataSourceType.Method)]
        public void Description_Validation_Successful(string displayName,
            string validDescription)
        {
            // Arrange.
            var createTaskViewModel =
                BuildCreateTaskViewModel(description: validDescription);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(createTaskViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidPriorityLevelData), DynamicDataSourceType.Method)]
        public void PriorityLevel_Validation_Successful(string displayName,
            int validPriorityLevel)
        {
            // Arrange.
            var createTaskViewModel =
                BuildCreateTaskViewModel(priorityLevel: validPriorityLevel);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(createTaskViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidTaskListIdData), DynamicDataSourceType.Method)]
        public void TaskListId_Validation_Successful(string displayName, 
            int validTaskListId)
        {
            // Arrange.
            var createTaskViewModel = 
                BuildCreateTaskViewModel(taskListId: validTaskListId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(createTaskViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetValidDescriptionData()
        {
            yield return new object[]
            {
                "Test case 1: Build CreateTaskViewModel object" +
                " and set Description field with MAX allowed valid value.",
                new string('*', 500)
            };

            yield return new object[]
            {
                "Test case 2: Build CreateTaskViewModel object" +
                " and set Description field with MIN allowed valid value.",
                new string("*")
            };

            yield return new object[]
            {
                "Test case 3: Build CreateTaskViewModel object" +
                " and set Description field with simple valid value.",
                new string("Default")
            };
        }

        private static IEnumerable<object[]> GetValidTaskListIdData()
        {
            yield return new object[]
            {
                "Test case 1: Build CreateTaskViewModel object" +
                " and set Task list id field with MAX allowed valid value.",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 2: Build CreateTaskViewModel object" +
                " and set Task list id field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build CreateTaskViewModel object" +
                " and set Task list id field with simple valid value.",
                100
            };
        }

        private static IEnumerable<object[]> GetValidPriorityLevelData()
        {
            yield return new object[]
            {
                "Test case 1: Build CreateTaskViewModel object" +
                " and set Priority level field with MAX allowed valid value.",
                4
            };

            yield return new object[]
            {
                "Test case 2: Build CreateTaskViewModel object" +
                " and set Priority level field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build CreateTaskViewModel object" +
                " and set Priority level field with simple valid value.",
                2
            };
        }
    }
}
