﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.TripViewModels;

namespace WebApiUnitTests.UpdateTripViewModelTests
{
    [TestClass]
    public class UpdateTripViewModel_Positive_Tests
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
        [DynamicData(nameof(Get_Valid_Trip_Id), DynamicDataSourceType.Method)]
        public void Test_Trip_Id_Validation(
            string testCaseDisplayName,
            int validId)
        {
            // Arrange
            UpdateTripViewModel updateTripViewModel =
                Get_Valid_UpdateTripViewModel(id: validId);

            // Act
            bool result = Validator.TryValidateObject(updateTripViewModel,
                new ValidationContext(updateTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_Trip_Title), DynamicDataSourceType.Method)]
        public void Test_Trip_Title_Validation(
            string testCaseDisplayName,
            string validTitle)
        {
            // Arrange
            UpdateTripViewModel updateTripViewModel =
                Get_Valid_UpdateTripViewModel(title: validTitle);

            // Act
            bool result = Validator.TryValidateObject(updateTripViewModel,
                new ValidationContext(updateTripViewModel, null, null),
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
            UpdateTripViewModel updateTripViewModel =
                Get_Valid_UpdateTripViewModel(description: validDescription);

            // Act
            bool result = Validator.TryValidateObject(updateTripViewModel,
                new ValidationContext(updateTripViewModel, null, null),
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
            UpdateTripViewModel updateTripViewModel =
                Get_Valid_UpdateTripViewModel(startsAt: validStartsAt);

            // Act
            bool result = Validator.TryValidateObject(updateTripViewModel,
                new ValidationContext(updateTripViewModel, null, null),
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
            UpdateTripViewModel updateTripViewModel =
                Get_Valid_UpdateTripViewModel(endsAt: validEndsAt);

            // Act
            bool result = Validator.TryValidateObject(updateTripViewModel,
                new ValidationContext(updateTripViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Valid_Trip_Id()
        {
            yield return new object[]
            {
                "Test case 1 : Test_Trip_Id_Validation was given Id with minimal" +
                " valid value that equals 1. Validation should be successful.",
                1
            };

            yield return new object[]
            {
                "Test case 2 : Test_Trip_Id_Validation was given Id with maximum" +
                " valid length that equals maximum number that is supported by integer." +
                " Validation should be successful.",
                int.MaxValue
            };
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

        UpdateTripViewModel Get_Valid_UpdateTripViewModel(
            int id = 1,
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

            return new UpdateTripViewModel()
            {
                Id = id,
                Title = title,
                Description = description,
                StartsAt = startsAt,
                EndsAt = endsAt
            };
        }
    }
}
