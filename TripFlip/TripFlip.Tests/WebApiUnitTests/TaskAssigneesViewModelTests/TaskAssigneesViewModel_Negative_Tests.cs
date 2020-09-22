using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.TaskViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.TaskAssigneesViewModelTests
{
    [TestClass]
    public class TaskAssigneesViewModel_Negative_Tests
    {
        static readonly IEnumerable<int> _defaultRouteSubscriberIds = new int[] { 1, 2, 3 };

        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_TaskId), DynamicDataSourceType.Method)]
        public void Test_TaskAssignees_TaskId_Validation(
            string testCaseDisplayName,
            int invalidTaskId)
        {
            // Arrange
            TaskAssigneesViewModel taskAssigneesViewModel =
                Get_TaskAssigneesViewModel(
                    taskId: invalidTaskId,
                    routeSubscriberIds: _defaultRouteSubscriberIds);

            // Act
            bool result = ModelValidator.IsValid(taskAssigneesViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_RouteSubscriberIds), DynamicDataSourceType.Method)]
        public void Test_TaskAssignees_RouteSubscriberIds_Validation(
            string testCaseDisplayName,
            IEnumerable<int> invalidRouteSubscriberIds)
        {
            // Arrange
            TaskAssigneesViewModel taskAssigneesViewModel =
                Get_TaskAssigneesViewModel(routeSubscriberIds: invalidRouteSubscriberIds);

            // Act
            bool result = ModelValidator.IsValid(taskAssigneesViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Invalid_TaskId()
        {
            yield return new object[]
            {
                "Test case 1 : Test_TaskAssignees_TaskId_Validation was given invalid" +
                " taskId value that is negative integer number (-1)." +
                " Validation should be failed.",
                -1
            };

            yield return new object[]
            {
                "Test case 2 : Test_TaskAssignees_TaskId_Validation was given invalid" +
                " taskId value that equals 0." +
                " Validation should be failed.",
                0
            };
        }

        static IEnumerable<object[]> Get_Invalid_RouteSubscriberIds()
        {
            yield return new object[]
            {
                "Test case 1 : Test_TaskAssignees_RouteSubscriberIds_Validation " +
                "was given invalid route subscriber ids collection that equals null." +
                " Validation should be failed.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_TaskAssignees_RouteSubscriberIds_Validation " +
                "was given invalid route subscriber ids collection that contains " +
                "duplicate values. " +
                " Validation should be failed.",
                new int[] {1, 1}
            };

            yield return new object[]
            {
                "Test case 3 : Test_TaskAssignees_RouteSubscriberIds_Validation " +
                "was given invalid route subscriber ids collection that contains " +
                "values that are out of allowed range." +
                " Validation should be failed.",
                new int[] {-1, 0}
            };
        }

        TaskAssigneesViewModel Get_TaskAssigneesViewModel(
            int taskId = 1,
            IEnumerable<int> routeSubscriberIds = null)
        {
            return new TaskAssigneesViewModel()
            {
                TaskId = taskId,
                RouteSubscriberIds = routeSubscriberIds
            };
        }
    }
}
