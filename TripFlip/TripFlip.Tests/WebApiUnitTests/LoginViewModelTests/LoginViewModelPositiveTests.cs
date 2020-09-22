using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TripFlip.ViewModels.UserViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.LoginViewModelTests
{
    [TestClass]
    public class LoginViewModelPositiveTests
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
            Assert.IsTrue(result);
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
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetTestEmailData()
        {
            yield return new object[]
            {
                "Test case 1: Create_LoginViewModel_Given_Valid_Email_min_length_and" +
                "_match_format_Validation_should_be_successful",
                "a@a.aa"
            };

            yield return new object[]
            {
                "Test case 2: Create_LoginViewModel_Given_Valid_Email_average_length_and" +
                "_match_format_Validation_should_be_failed",
                "aaaaaaaa@aaaaa.aaa"
            };

            yield return new object[]
            {
                "Test case 3: Create_LoginViewModel_Given_Valid_Email_max_length_and" +
                "_match_format_Validation_should_be_successful",
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaa@a.aa"
            };

            yield return new object[]
            {
                "Test case 4: Create_LoginViewModel_Given_Valid_Email_average_length_and" +
                "_match_format_Validation_should_be_successful",
                "aaaaaaaaaaaaaaaaaaa@a.aa"
            };
        }

        private static IEnumerable<object[]> GetTestPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Create_LoginViewModel_Given_Valid_Password_min_length_" +
                "Validation_should_be_successful",
                new string('*', 8)
            };

            yield return new object[]
            {
                "Test case 2: Create_LoginViewModel_Given_Not_valid_Password_max_length_" +
                "Validation_should_be_successful",
                new string('*', 100)
            };

            yield return new object[]
            {
                "Test case 3: Create_LoginViewModel_Given_Not_valid_Password_average_length_" +
                "Validation_should_be_successful",
                new string('*', 20)
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
