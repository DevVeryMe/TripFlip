using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemListViewModels;

namespace WebApiUnitTests.ItemList
{
    [TestClass]
    public class UpdateItemListViewModel_Positive_Tests
    {
        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_ItemList_Title), DynamicDataSourceType.Method)]
        public void Test_ItemList_Title_Validation(
            string testCaseDisplayName,
            string validTitle)
        {
            // Arrange
            UpdateItemListViewModel updateItemListViewModel =
                Get_Valid_UpdateItemListViewModel(title: validTitle);

            // Act
            bool result = Validator.TryValidateObject(updateItemListViewModel,
                new ValidationContext(updateItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_ItemList_Id), DynamicDataSourceType.Method)]
        public void Test_ItemList_Id_Validation(
            string testCaseDisplayName,
            int validId)
        {
            // Arrange
            UpdateItemListViewModel updateItemListViewModel =
                Get_Valid_UpdateItemListViewModel(id: validId);

            // Act
            bool result = Validator.TryValidateObject(updateItemListViewModel,
                new ValidationContext(updateItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Valid_ItemList_Title()
        {
            yield return new object[]
            {
                "Test case 1 : Test_ItemList_Title_Validation was given title with minimal" +
                " valid length that equals 1. Validation should be successful.",
                new string('x', 1)
            };

            yield return new object[]
            {
                "Test case 2 : Test_ItemList_Title_Validation was given title with maximum" +
                " valid length that equals 100. Validation should be successful.",
                new string('x', 100)
            };
        }

        static IEnumerable<object[]> Get_Valid_ItemList_Id()
        {
            yield return new object[]
            {
                "Test case 1 : Test_ItemList_Title_Validation was given Id with minimal" +
                " valid value that equals 1. Validation should be successful.",
                1
            };

            yield return new object[]
            {
                "Test case 2 : Test_ItemList_Title_Validation was given Id with maximum" +
                " valid length that equals maximum number that is supported by integer." +
                " Validation should be successful.",
                int.MaxValue
            };
        }

        UpdateItemListViewModel Get_Valid_UpdateItemListViewModel(
            int id = 1,
            string title = "Valid item list title.")
        {
            return new UpdateItemListViewModel()
            {
                Id = id,
                Title = title
            };
        }
    }
}
