using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.TripViewModels;

namespace WebApiUnitTests.CreateTripViewModelTests
{
    [TestClass]
    public class CreateTripViewModel_Positive_Tests
    {
        static readonly DateTimeOffset _defaultStartsAt = new DateTimeOffset(
                    year: 2030, month: 8, day: 12,
                    hour: 10, minute: 0, second: 0,
                    offset: TimeSpan.Zero);
        static readonly DateTimeOffset _defaultEndsAt = new DateTimeOffset(
                    year: 2030, month: 9, day: 12,
                    hour: 10, minute: 0, second: 0,
                    offset: TimeSpan.Zero);

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_Trip_Title), DynamicDataSourceType.Method)]
        public void Test_Trip_Title_Validation(
            string testCaseDisplayName,
            string validTitle)
        {
            // Arrange
            CreateTripViewModel createTripViewModel =
                Get_Valid_CreateTripViewModel(title: validTitle);

            // Act
            bool result = Validator.TryValidateObject(createTripViewModel,
                new ValidationContext(createTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_Trip_Description), DynamicDataSourceType.Method)]
        public void Test_Trip_Description_Validation(
            string testCaseDisplayName,
            string validDescription)
        {
            // Arrange
            CreateTripViewModel createTripViewModel =
                Get_Valid_CreateTripViewModel(description: validDescription);

            // Act
            bool result = Validator.TryValidateObject(createTripViewModel,
                new ValidationContext(createTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_Trip_StartsAt), DynamicDataSourceType.Method)]
        public void Test_Trip_StartsAt_Validation(
            string testCaseDisplayName,
            DateTimeOffset? validStartsAt)
        {
            // Arrange
            CreateTripViewModel createTripViewModel =
                Get_Valid_CreateTripViewModel(startsAt: validStartsAt);

            // Act
            bool result = Validator.TryValidateObject(createTripViewModel,
                new ValidationContext(createTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_Trip_EndsAt), DynamicDataSourceType.Method)]
        public void Test_Trip_EndsAt_Validation(
            string testCaseDisplayName,
            DateTimeOffset? validEndsAt)
        {
            // Arrange
            CreateTripViewModel createTripViewModel =
                Get_Valid_CreateTripViewModel(endsAt: validEndsAt);

            // Act
            bool result = Validator.TryValidateObject(createTripViewModel,
                new ValidationContext(createTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Valid_Trip_Title()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_Title_Validation was given title with minimal" +
                " valid length that equals 1. Validation should be successful.",
                new string('x', 1)
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_Title_Validation was given title with maximum" +
                " valid length that equals 100. Validation should be successful.",
                new string('x', 100)
            };
        }

        static IEnumerable<object[]> Get_Valid_Trip_Description()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_Description_Validation was given title with minimal" +
                " valid length that equals 1. Validation should be successful.",
                new string('x', 1)
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_Description_Validation was given title with maximum" +
                " valid length that equals 500. Validation should be successful.",
                new string('x', 500)
            };
        }

        static IEnumerable<object[]> Get_Valid_Trip_StartsAt()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_StartsAt_Validation was given null StartsAt value." +
                " Validation should be successful.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_StartsAt_Validation was given valid StartsAt value" +
                " that is later than the current DateTimeOffset. Validation should be successful.",
                _defaultStartsAt
            };
        }

        static IEnumerable<object[]> Get_Valid_Trip_EndsAt()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_EndsAt_Validation was given null EndsAt value." +
                " Validation should be successful.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_StartsAt_Validation was given valid EndsAt value" +
                " that is later than the current DateTimeOffset. Validation should be successful.",
                _defaultEndsAt
            };
        }

        CreateTripViewModel Get_Valid_CreateTripViewModel(
            string title = "Valid trip title.",
            string description = "Valid trip description.",
            DateTimeOffset? startsAt = null,
            DateTimeOffset? endsAt = null)
        {
            if (!startsAt.HasValue)
            {
                startsAt = _defaultStartsAt;
            }
            if (!endsAt.HasValue)
            {
                endsAt = _defaultEndsAt;
            }

            return new CreateTripViewModel()
            {
                Title = title,
                Description = description,
                StartsAt = startsAt,
                EndsAt = endsAt
            };
        }
    }
}
