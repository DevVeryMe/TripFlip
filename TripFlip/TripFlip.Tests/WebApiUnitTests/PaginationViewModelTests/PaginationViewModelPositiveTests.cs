using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels;

namespace WebApiUnitTests.PaginationViewModelTests
{
    [TestClass]
    public class PaginationViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestPageNumberData), DynamicDataSourceType.Method)]
        public void TestPaginationPageNumberValidation(string displayName, int? pageNumber)
        {
            // Arrange
            var paginationViewModel = GetPaginationViewModel(pageNumber: pageNumber);

            // Act
            var result = Validator.TryValidateObject(paginationViewModel,
                new ValidationContext(paginationViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

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
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetTestPageNumberData()
        {
            yield return new object[]
            {
                "Test case 1: Create_PaginationViewModel_Given_Valid_PageNumber_equals_" +
                "min_value_Validation_should_be_successful",
                1
            };

            yield return new object[]
            {
                "Test case 2: Create_PaginationViewModel_Given_Valid_PageNumber_equals_" +
                "max_value_Validation_should_be_successful",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 3: Create_PaginationViewModel_Given_Valid_PageNumber_equals_" +
                "null_Validation_should_be_successful",
                null
            };
        }

        private static IEnumerable<object[]> GetTestPageSizeData()
        {
            yield return new object[]
            {
                "Test case 1: Create_PaginationViewModel_Given_Valid_PageSize_equals_" +
                "min_value_1_Validation_should_be_successful",
                1
            };

            yield return new object[]
            {
                "Test case 2: Create_PaginationViewModel_Given_Valid_PageSize_equals_" +
                "max_value_50_Validation_should_be_successful",
                50
            };

            yield return new object[]
            {
                "Test case 3: Create_PaginationViewModel_Given_Valid_PageSize_equals_" +
                "null_Validation_should_be_successful",
                null
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
