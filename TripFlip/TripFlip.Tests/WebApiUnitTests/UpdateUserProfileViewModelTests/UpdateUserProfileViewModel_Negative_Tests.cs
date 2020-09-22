using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.UserViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateUserProfileViewModelTests
{
    [TestClass]
    public class UpdateUserProfileViewModel_Negative_Tests
    {
        readonly string _defaultValidEmail = "sample@mail.com";

        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_Email), DynamicDataSourceType.Method)]
        public void Test_UpdateUserProfile_Email_Validation(
            string testCaseDisplayName,
            string invalidEmail)
        {
            // Arrange
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_UpdateUserProfileViewModel(email: invalidEmail);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [TestMethod]
        public void Test_UpdateUserProfile_FirstName_Validation()
        {
            // Arrange
            string invalidFirstName = new string('*', 51);
            string testCaseDisplayName = "FirstName property was given invalid " +
                "string value that exceeds the max length of 50. Validation should be failed.";
            UpdateUserProfileViewModel updateUserProfileViewModel = 
                Get_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    firstName: invalidFirstName);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [TestMethod]
        public void Test_UpdateUserProfile_LastName_Validation()
        {
            // Arrange
            string invalidLastName = new string('*', 51);
            string testCaseDisplayName = "LastName property was given invalid " +
                "string value that exceeds the max length of 50. Validation should be failed.";
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    lastName: invalidLastName);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [TestMethod]
        public void Test_UpdateUserProfile_AboutMe_Validation()
        {
            // Arrange
            string invalidAboutMe = new string('*', 301);
            string testCaseDisplayName = "AboutMe property was given invalid " +
                "string value that exceeds the max length of 300. Validation should be failed.";
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    aboutMe: invalidAboutMe);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [TestMethod]
        public void Test_UpdateUserProfile_Gender_Validation()
        {
            // Arrange
            UserGender invalidGender = (UserGender)999;
            string testCaseDisplayName = "Gender property was given invalid " +
                "value which is not defined in the UserGender Enum. Validation should be failed.";
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    gender: invalidGender);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [TestMethod]
        public void Test_UpdateUserProfile_BirthDate_Validation()
        {
            // Arrange
            DateTimeOffset invalidBirthDate = new DateTimeOffset(
                day: 1, month: 1, year: 9999, hour:0, minute: 1, second:1,
                offset: TimeSpan.Zero);
            string testCaseDisplayName = "BirthDate property was given invalid " +
                "value which is later or equals the current time. Validation should be failed.";
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    birthDate: invalidBirthDate);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Invalid_Email()
        {
            yield return new object[]
            {
                "Test case 1 : Test_UpdateUserProfile_Email_Validation was given " +
                "invalid Email that equals null. Validation should be failed.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_UpdateUserProfile_Email_Validation was given " +
                "invalid Email string that is empty. Validation should be failed.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3 : Test_UpdateUserProfile_Email_Validation was given " +
                "invalid Email string with the length of 101 (which exceeds allowed " +
                "string length of 320 characters). Validation should be failed.",
                new string('*', 321) + "@mail.com"
            };

            yield return new object[]
            {
                "Test case 4 : Test_UpdateUserProfile_Email_Validation was given " +
                "Email string with invalid email format. Validation should be failed.",
                new string('*', 20)
            };
        }

        UpdateUserProfileViewModel Get_UpdateUserProfileViewModel(
            string email = default,
            string firstName = default,
            string lastName = default,
            string aboutMe = default,
            UserGender? gender = default,
            DateTimeOffset? birthDate = default)
        {
            return new UpdateUserProfileViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                AboutMe = aboutMe,
                Gender = gender,
                BirthDate = birthDate
            };
        }
    }
}
