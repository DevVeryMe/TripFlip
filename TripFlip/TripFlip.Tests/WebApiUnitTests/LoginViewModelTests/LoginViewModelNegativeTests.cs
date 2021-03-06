﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TripFlip.ViewModels.UserViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.LoginViewModelTests
{
    [TestClass]
    public class LoginViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestEmailData), DynamicDataSourceType.Method)]
        public void TestLoginEmailValidation(string displayName, string email)
        {
            // Arrange
            var loginViewModel = GetLoginViewModel(email: email);

            // Act
            var result = ModelValidator.IsValid(loginViewModel);

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestPasswordData), DynamicDataSourceType.Method)]
        public void TestLoginPasswordValidation(string displayName, string password)
        {
            // Arrange
            var loginViewModel = GetLoginViewModel(password: password);

            // Act
            var result = ModelValidator.IsValid(loginViewModel);

            // Assert
            Assert.IsFalse(result);
        }

        private static IEnumerable<object[]> GetTestEmailData()
        {
            yield return new object[]
            {
                "Test case 1: Create_LoginViewModel_Given_Not_valid_Email_equals_null_" +
                "Validation_should_be_failed",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_LoginViewModel_Given_Not_valid_Email_equals_empty_string_" +
                "Validation_should_be_failed",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_LoginViewModel_Given_Not_valid_Email_too_long_length_" +
                "Validation_should_be_failed",
                new string('a', 320) + "@a.aaa"
            };

            yield return new object[]
            {
                "Test case 4: Create_LoginViewModel_Given_Not_valid_Email_not_correct_format_" +
                "Validation_should_be_failed",
                new string('*', 20)
            };
        }

        private static IEnumerable<object[]> GetTestPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Create_LoginViewModel_Given_Not_valid_Password_equals_null_" +
                "Validation_should_be_failed",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_LoginViewModel_Given_Not_valid_Password_equals_empty_string_" +
                "Validation_should_be_failed",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_LoginViewModel_Given_Not_valid_Password_too_long_length_" +
                "Validation_should_be_failed",
                new string('*', 101)
            };

            yield return new object[]
            {
                "Test case 4: Create_LoginViewModel_Given_Not_valid_Password_too_short_length_" +
                "Validation_should_be_failed",
                new string('*', 7)
            };
        }

        private LoginViewModel GetLoginViewModel(
            string email = "string@string.com",
            string password = "String1@")
        {
            return new LoginViewModel()
            {
                Email = email,
                Password = password
            };
        }
    }
}
