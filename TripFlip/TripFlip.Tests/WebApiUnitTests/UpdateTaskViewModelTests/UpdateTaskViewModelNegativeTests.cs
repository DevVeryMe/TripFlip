using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateTaskViewModelTests
{
    [TestClass]
    public class UpdateTaskViewModelNegativeTests : UpdateTaskViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidDescriptionData), DynamicDataSourceType.Method)]
        public void Description_IsNotValid_ExceptionThrown(string displayName,
            string notValidDescription)
        {
            // Arrange.
            var updateTaskViewModel = BuildUpdateTaskViewModel(description: notValidDescription);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateTaskViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidIdData), DynamicDataSourceType.Method)]
        public void Id_IsNotValid_ExceptionThrown(string displayName, int notValidId)
        {
            // Arrange.
            var updateTaskViewModel = BuildUpdateTaskViewModel(id: notValidId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateTaskViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidPriorityLevelData), DynamicDataSourceType.Method)]
        public void PriorityLevel_IsNotValid_ExceptionThrown(string displayName,
            int notValidPriorityLevel)
        {
            // Arrange.
            var updateTaskViewModel = BuildUpdateTaskViewModel(priorityLevel: notValidPriorityLevel);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateTaskViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetInvalidDescriptionData()
        {
            yield return new object[]
            {
                "Test case 1: Test UpdateTaskViewModel validation" +
                " if Description set to null. Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test UpdateTaskViewModel validation" +
                " if Description set to empty string. Validation should fail.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Test UpdateTaskViewModel validation" +
                " if Description length is more than allowed (> 500). Validation should fail.",
                new string('*', 501)
            };
        }

        private static IEnumerable<object[]> GetInvalidIdData()
        {
            yield return new object[]
            {
                "Test case 1: Test UpdateTaskViewModel validation" +
                " if Id is zero. Validation should fail.",
                0
            };

            yield return new object[]
            {
                "Test case 2: Test UpdateTaskViewModel validation" +
                " if Id is negative number. Validation should fail.",
                -1
            };
        }

        private static IEnumerable<object[]> GetInvalidPriorityLevelData()
        {
            yield return new object[]
            {
                "Test case 1: Test UpdateTaskViewModel validation" +
                " if Priority level is less than allowed (< 1). Validation should fail.",
                0
            };

            yield return new object[]
            {
                "Test case 2: Test UpdateTaskViewModel validation" +
                " if Priority level is more than allowed (> 4). Validation should fail.",
                5
            };
        }
    }
}
