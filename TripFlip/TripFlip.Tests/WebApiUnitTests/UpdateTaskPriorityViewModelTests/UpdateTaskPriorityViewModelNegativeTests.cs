using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateTaskPriorityViewModelTests
{
    [TestClass]
    public class UpdateTaskPriorityViewModelNegativeTests : UpdateTaskPriorityViewModelTestsBase
    {
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
