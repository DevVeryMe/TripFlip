using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.RegisterUserViewModelTests
{
    [TestClass]
    public class RegisterUserViewModelNegativeTests
        : RegisterUserViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestInvalidEmailData), DynamicDataSourceType.Method)]
        public void Email_IsNotValid_ExceptionThrown(string displayName,
            string invalidEmail)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(email: invalidEmail);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestInvalidPasswordData), DynamicDataSourceType.Method)]
        public void Password_IsNotValid_ExceptionThrown(string displayName,
            string invalidPassword)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(password: invalidPassword);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestInvalidPasswordConfirmationData),
            DynamicDataSourceType.Method)]
        public void PasswordConfirmation_IsNotValid_ExceptionThrown(string displayName,
            string invalidPasswordConfirmation)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(
                    passwordConfirmation: invalidPasswordConfirmation);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [TestMethod]
        public void PasswordConfirmation_MissMatchPassword_ExceptionThrown()
        {
            // Arrange.
            var validPassword = "TestPassword@1";
            var invalidPasswordConfirmation = "TestPassword@2";

            var registerUserViewModel = BuildRegisterUserViewModel(
                password: validPassword,
                passwordConfirmation: invalidPasswordConfirmation);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        [TestMethod]
        public void FirstName_TooLong_ExceptionThrown()
        {
            // Arrange.
            var invalidFirstName = new string('*', 51);

            var registerUserViewModel =
                BuildRegisterUserViewModel(firstName: invalidFirstName);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        [TestMethod]
        public void LastName_TooLong_ExceptionThrown()
        {
            // Arrange.
            var invalidLastName = new string('*', 51);

            var registerUserViewModel =
                BuildRegisterUserViewModel(lastName: invalidLastName);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        [TestMethod]
        public void AboutMe_TooLong_ExceptionThrown()
        {
            // Arrange.
            var invalidAboutMe = new string('*', 301);

            var registerUserViewModel =
                BuildRegisterUserViewModel(aboutMe: invalidAboutMe);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidGenderData), DynamicDataSourceType.Method)]
        public void Gender_IsNotValid_ExceptionThrown(string displayName,
            int gender)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(gender: gender);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [TestMethod]
        public void BirthDate_IsLaterThanNow_ExceptionThrown()
        {
            // Arrange.
            var invalidBirthDate =
                new DateTimeOffset(DateTime.Now.AddDays(1));


            var registerUserViewModel =
                BuildRegisterUserViewModel(birthDate: invalidBirthDate);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }

        private static IEnumerable<object[]> GetTestInvalidEmailData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if Email field is null." +
                " Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if Email field is empty." +
                " Validation should fail.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if Email length is more than allowed." +
                " Validation should fail.",
                new string('a', 320) + "@" + "a.ua"
            };

            yield return new object[]
            {
                "Test case 4: Test RegisterUserViewModel validation" +
                " if Email has invalid format." +
                " Validation should fail.",
                new string('*', 10)
            };
        }

        private static IEnumerable<object[]> GetTestInvalidPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if Password field is null." +
                " Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if Password field is empty." +
                " Validation should fail.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if Password is too long." +
                " Validation should fail.",
                new string('*', 101)
            };

            yield return new object[]
            {
                "Test case 4: Test RegisterUserViewModel validation" +
                " if Password is too short." +
                " Validation should fail.",
                new string('*', 7)
            };

            yield return new object[]
            {
                "Test case 5: Test RegisterUserViewModel validation" +
                " if Password doesn't contain any number." +
                " Validation should fail.",
                "TestPassword@"
            };

            yield return new object[]
            {
                "Test case 6: Test RegisterUserViewModel validation" +
                " if Password doesn't contain any special symbol." +
                " Validation should fail.",
                "TestPassword1"
            };

            yield return new object[]
            {
                "Test case 7: Test RegisterUserViewModel validation" +
                " if Password doesn't contain any upper case letter." +
                " Validation should fail.",
                "test_password1"
            };
        }

        private static IEnumerable<object[]> GetTestInvalidPasswordConfirmationData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if PasswordConfirmation field is null." +
                " Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if PasswordConfirmation field is empty." +
                " Validation should fail.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if PasswordConfirmation is too long." +
                " Validation should fail.",
                new string('*', 101)
            };

            yield return new object[]
            {
                "Test case 4: Test RegisterUserViewModel validation" +
                " if PasswordConfirmation is too short." +
                " Validation should fail.",
                new string('*', 7)
            };
        }

        private static IEnumerable<object[]> GetInvalidGenderData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if Gender value is less than allowed." +
                " Validation should fail.",
                0
            };

            yield return new object[]
            {
                "Test case 2: Test UpdateTaskViewModel validation" +
                " if Gender value is more than allowed." +
                " Validation should fail.",
                3
            };
        }
    }
}
