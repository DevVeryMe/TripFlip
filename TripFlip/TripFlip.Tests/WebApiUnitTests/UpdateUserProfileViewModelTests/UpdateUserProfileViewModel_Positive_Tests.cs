using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.UserViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.UpdateUserProfileViewModelTests
{
    [TestClass]
    public class UpdateUserProfileViewModel_Positive_Tests
    {
        readonly string _defaultValidEmail = "sample@mail.com";
        static readonly int _nameMaxLength = 50;
        static readonly int _aboutMeMaxLength = 300;

        [TestMethod]
        public void Test_UpdateUserProfile_Email_Validation()
        {
            // Arrange
            string testCaseDisplayName = "Email property was given value that is of " +
                "correct length and of valid email format. Validation should be successful.";
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_Valid_UpdateUserProfileViewModel(email: _defaultValidEmail);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_FirstName), DynamicDataSourceType.Method)]
        public void Test_UpdateUserProfile_FirstName_Validation(
            string testCaseDisplayName,
            string validFirstName)
        {
            // Arrange
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_Valid_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    firstName: validFirstName);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_LastName), DynamicDataSourceType.Method)]
        public void Test_UpdateUserProfile_LastName_Validation(
            string testCaseDisplayName,
            string validLastName)
        {
            // Arrange
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_Valid_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    lastName: validLastName);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_AboutMe), DynamicDataSourceType.Method)]
        public void Test_UpdateUserProfile_AboutMe_Validation(
            string testCaseDisplayName,
            string validAboutMe)
        {
            // Arrange
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_Valid_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    aboutMe: validAboutMe);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_UserGender), DynamicDataSourceType.Method)]
        public void Test_UpdateUserProfile_UserGender_Validation(
            string testCaseDisplayName,
            UserGender? validUserGender)
        {
            // Arrange
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_Valid_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    userGender: validUserGender);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_BirthDate), DynamicDataSourceType.Method)]
        public void Test_UpdateUserProfile_BirthDate_Validation(
            string testCaseDisplayName,
            DateTimeOffset? validBirthDate)
        {
            // Arrange
            UpdateUserProfileViewModel updateUserProfileViewModel =
                Get_Valid_UpdateUserProfileViewModel(
                    email: _defaultValidEmail,
                    birthDate: validBirthDate);

            // Act
            bool result = ModelValidator.IsValid(updateUserProfileViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Valid_FirstName()
        {
            yield return new object[]
            {
                "Test case 1 : Test_UpdateUserProfile_FirstName_Validation was given " +
                "valid FirstName value that is null. Validation should be successful.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_UpdateUserProfile_FirstName_Validation was given " +
                "valid FirstName string that is empty. Validation should be successful.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3 : Test_UpdateUserProfile_FirstName_Validation was given " +
                $"FirstName value with maximum valid length that equals {_nameMaxLength}. " +
                "Validation should be successful.",
                new string('x', _nameMaxLength)
            };
        }

        static IEnumerable<object[]> Get_Valid_LastName()
        {
            yield return new object[]
            {
                "Test case 1 : Test_UpdateUserProfile_LastName_Validation was given " +
                "valid LastName value that is null. Validation should be successful.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_UpdateUserProfile_LastName_Validation was given " +
                "valid LastName string that is empty. Validation should be successful.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3 : Test_UpdateUserProfile_LastName_Validation was given " +
                $"LastName value with maximum valid length that equals {_nameMaxLength}. " +
                "Validation should be successful.",
                new string('x', _nameMaxLength)
            };
        }

        static IEnumerable<object[]> Get_Valid_AboutMe()
        {
            yield return new object[]
            {
                "Test case 1 : Test_UpdateUserProfile_AboutMe_Validation was given " +
                "valid AboutMe value that is null. Validation should be successful.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_UpdateUserProfile_AboutMe_Validation was given " +
                "valid AboutMe string that is empty. Validation should be successful.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3 : Test_UpdateUserProfile_AboutMe_Validation was given " +
                $"AboutMe value with maximum valid length that equals {_aboutMeMaxLength}. " +
                "Validation should be successful.",
                new string('x', _aboutMeMaxLength)
            };
        }

        static IEnumerable<object[]> Get_Valid_UserGender()
        {
            yield return new object[]
            {
                "Test case 1 : Test_UpdateUserProfile_UserGender_Validation was given " +
                "valid UserGender value that is null. Validation should be successful.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_UpdateUserProfile_UserGender_Validation was given " +
                "valid UserGender value that is defined in the UserGender Enum. " +
                "Validation should be successful.",
                UserGender.Female
            };
        }

        static IEnumerable<object[]> Get_Valid_BirthDate()
        {
            yield return new object[]
            {
                "Test case 1 : Test_UpdateUserProfile_BirthDate_Validation was given " +
                "valid BirthDate value that is null. Validation should be successful.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_UpdateUserProfile_BirthDate_Validation was given " +
                "valid BirthDate value that is earlier than the current date & time. " +
                "Validation should be successful.",
                new DateTimeOffset(
                    day: 1, month: 1, year: 1950, hour:1, minute: 1, second:1,
                    offset: TimeSpan.Zero)
        };
        }

        UpdateUserProfileViewModel Get_Valid_UpdateUserProfileViewModel(
            string email = default,
            string firstName = default,
            string lastName = default,
            string aboutMe = default,
            UserGender? userGender = default,
            DateTimeOffset? birthDate = default)
        {
            return new UpdateUserProfileViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                AboutMe = aboutMe,
                Gender = userGender,
                BirthDate = birthDate
            };
        }
    }
}
