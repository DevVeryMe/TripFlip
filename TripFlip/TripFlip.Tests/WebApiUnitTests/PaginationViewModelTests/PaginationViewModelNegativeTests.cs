using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels;

namespace WebApiUnitTests.PaginationViewModelTests
{
    [TestClass]
    public class PaginationViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestPageSizeData), DynamicDataSourceType.Method)]
        public void TestPaginationPageSizeValidation(string displayName, int? pageSize)
        {
            // Arrange
            var paginationViewModel = GetPaginationViewModel(pageSize: pageSize);

            // Act
            var result = Validator.TryValidateObject(paginationViewModel,
                new ValidationContext(paginationViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_PaginationViewModel_Given_Not_valid_PageNumber_equals_min_value_0_should_be_failed()
        {
            // Arrange
            var paginationViewModel = GetPaginationViewModel(pageSize: 0);

            // Act
            var result = Validator.TryValidateObject(paginationViewModel,
                new ValidationContext(paginationViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        private static IEnumerable<object[]> GetTestPageSizeData()
        {
            yield return new object[]
            {
                "Test case 1: Create_PaginationViewModel_Given_Not_valid_PageSize_equals_" +
                "less_than_min_value_0_Validation_should_be_failed",
                0
            };

            yield return new object[]
            {
                "Test case 2: Create_PaginationViewModel_Given_Not_valid_PageSize_equals_" +
                "greater_than_max_value_51_Validation_should_be_failed",
                51
            };
        }

        private static PaginationViewModel GetPaginationViewModel(
            int? pageSize = null, int? pageNumber = null)
        {
            return new PaginationViewModel()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
        }
    }
}
