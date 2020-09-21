using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.ItemAssigneeViewModelTests
{
    [TestClass]
    public class ItemAssigneeViewModelPositiveTests
        : ItemAssigneeViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetValidItemIdData), DynamicDataSourceType.Method)]
        public void ItemId_Validation_Successful(string displayName,
            int validItemId)
        {
            // Arrange.
            var validRouteSubscriberIds = new int[] { };

            var itemAssigneesViewModel =
                BuildItemAssigneesViewModel(validRouteSubscriberIds, validItemId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(itemAssigneesViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidRouteSubscriberIdsData), DynamicDataSourceType.Method)]
        public void RouteSubscriberIds_Validation_Successful(string displayName,
            IEnumerable<int> validRouteSubscriberIds)
        {
            // Arrange.
            var itemAssigneesViewModel =
                BuildItemAssigneesViewModel(routeSubscriberIds: validRouteSubscriberIds);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(itemAssigneesViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetValidItemIdData()
        {
            yield return new object[]
            {
                "Test case 1: Build ItemAssigneeViewModel object" +
                " and set ItemId field with MAX allowed valid value.",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 2: Build ItemAssigneeViewModel object" +
                " and set ItemId field with MIN allowed valid value.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Build ItemAssigneeViewModel object" +
                " and set ItemId field with simple valid value.",
                100
            };
        }

        private static IEnumerable<object[]> GetValidRouteSubscriberIdsData()
        {
            yield return new object[]
            {
                "Test case 1: Build ItemAssigneeViewModel object" +
                " and add to RouteSubscriberIds array MAX allowed valid value.",
                new int[] { int.MaxValue }
            };

            yield return new object[]
            {
                "Test case 2: Build ItemAssigneeViewModel object" +
                " and add to RouteSubscriberIds array MIN allowed valid value.",
                new int[] { 1 }
            };

            yield return new object[]
            {
                "Test case 3: Build ItemAssigneeViewModel object" +
                " and fill RouteSubscriberIds array with simple valid value.",
                new int[] { 2, 3, 4 }
            };

            yield return new object[]
            {
                "Test case 4: Build ItemAssigneeViewModel object" +
                " and make RouteSubscriberIds array empty.",
                new int[] { }
            };
        }
    }
}
