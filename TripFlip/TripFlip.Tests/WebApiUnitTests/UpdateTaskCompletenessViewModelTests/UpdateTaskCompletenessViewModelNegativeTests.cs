using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateTaskCompletenessViewModelTests
{
    [TestClass]
    public class UpdateTaskCompletenessViewModelNegativeTests
        : UpdateTaskCompletenessViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidIdData), DynamicDataSourceType.Method)]
        public void Id_IsNotValid_ExceptionThrown(string displayName, int notValidId)
        {
            // Arrange.
            var updateTaskViewModel = 
                BuildUpdateTaskCompletenessViewModel(id: notValidId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(updateTaskViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetInvalidIdData()
        {
            yield return new object[]
            {
                "Test case 1: Test UpdateTaskCompletenessViewModel validation" +
                " if Id is zero. Validation should fail.",
                0
            };

            yield return new object[]
            {
                "Test case 2: Test UpdateTaskCompletenessViewModel validation" +
                " if Id is negative number. Validation should fail.",
                -1
            };
        }
    }
}
