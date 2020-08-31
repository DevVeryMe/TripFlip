﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.TripViewModels;

namespace WebApiUnitTests.CreateTripViewModelTests
{
    [TestClass]
    public class CreateTripViewModel_Negative_Tests
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
        [DynamicData(nameof(Get_Invalid_Trip_Title), DynamicDataSourceType.Method)]
        public void Test_Trip_Title_Validation(
            string testCaseDisplayName,
            string invalidTitle)
        {
            // Arrange
            CreateTripViewModel createTripViewModel =
                Get_CreateTripViewModel(title: invalidTitle);

            // Act
            bool result = Validator.TryValidateObject(createTripViewModel,
                new ValidationContext(createTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_Trip_Description), DynamicDataSourceType.Method)]
        public void Test_Trip_Description_Validation(
            string testCaseDisplayName,
            string invalidDescription)
        {
            // Arrange
            CreateTripViewModel createTripViewModel =
                Get_CreateTripViewModel(description: invalidDescription);

            // Act
            bool result = Validator.TryValidateObject(createTripViewModel,
                new ValidationContext(createTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_Trip_StartsAt), DynamicDataSourceType.Method)]
        public void Test_Trip_StartsAt_Validation(
            string testCaseDisplayName,
            DateTimeOffset? invalidStartsAt)
        {
            // Arrange
            CreateTripViewModel createTripViewModel =
                Get_CreateTripViewModel(startsAt: invalidStartsAt);

            // Act
            bool result = Validator.TryValidateObject(createTripViewModel,
                new ValidationContext(createTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_Trip_EndsAt), DynamicDataSourceType.Method)]
        public void Test_Trip_EndsAt_Validation(
            string testCaseDisplayName,
            DateTimeOffset? invalidEndsAt)
        {
            // Arrange
            CreateTripViewModel createTripViewModel =
                Get_CreateTripViewModel(endsAt: invalidEndsAt);

            // Act
            bool result = Validator.TryValidateObject(createTripViewModel,
                new ValidationContext(createTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Invalid_Trip_Title()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_Title_Validation was given invalid Title equals null." +
                " Validation should be failed.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_Title_Validation was given invalid empty Title string." +
                " Validation should be failed.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3 : Test_Trip_Title_Validation was given invalid Title string" +
                " with the length of 101 (which exceeds allowed string length of 100 characters)." +
                " Validation should be failed.",
                new string('*', 101)
            };
        }

        static IEnumerable<object[]> Get_Invalid_Trip_Description()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_Description_Validation was given invalid Description equals null." +
                " Validation should be failed.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_Description_Validation was given invalid empty Description string." +
                " Validation should be failed.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3 : Test_Trip_Description_Validation was given invalid Description string" +
                " with the length of 501 (which exceeds allowed string length of 500 characters)." +
                " Validation should be failed.",
                new string('*', 501)
            };
        }

        static IEnumerable<object[]> Get_Invalid_Trip_StartsAt()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_StartsAt_Validation was given invalid " +
                "StartsAt date that is earlier than the current date." +
                " Validation should be failed.",
                new DateTimeOffset(
                    year: 1980, month: 1, day: 1,
                    hour: 10, minute: 0, second: 0,
                    offset: TimeSpan.Zero)
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_StartsAt_Validation was given invalid " +
                "StartsAt DateTimeOffset that equals current DateTimeOffset" +
                " Validation should be failed.",
                DateTimeOffset.Now
            };
        }

        static IEnumerable<object[]> Get_Invalid_Trip_EndsAt()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_EndsAt_Validation was given invalid " +
                "EndsAt date that is earlier than the current date." +
                " Validation should be failed.",
                new DateTimeOffset(
                    year: 1980, month: 1, day: 1,
                    hour: 10, minute: 0, second: 0,
                    offset: TimeSpan.Zero)
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_EndsAt_Validation was given invalid " +
                "EndsAt DateTimeOffset that equals current DateTimeOffset" +
                " Validation should be failed.",
                DateTimeOffset.Now
            };

            yield return new object[]
            {
                "Test case 3 : Test_Trip_EndsAt_Validation was given invalid " +
                "EndsAt DateTimeOffset that equals StartsAt DateTimeOffset" +
                " Validation should be failed.",
                _defaultStartsAt
            };
        }
        
        CreateTripViewModel Get_CreateTripViewModel(
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
