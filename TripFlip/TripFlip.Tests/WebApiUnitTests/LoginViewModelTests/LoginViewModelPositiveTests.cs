using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.UserViewModels;

namespace WebApiUnitTests.LoginViewModelTests
{
    public class LoginViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestEmailData), DynamicDataSourceType.Method)]
        public void TestLoginEmailValidation(string displayName, string email)
        {
            // Arrange
            var createItemViewModel = GetLoginViewModel(email: email);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestPasswordData), DynamicDataSourceType.Method)]
        public void TestLoginPasswordValidation(string displayName, string password)
        {
            // Arrange
            var createItemViewModel = GetLoginViewModel(password: password);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

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
                "Test case 3: Create_LoginViewModel_Given_Valid_Email_average_length_and" +
                "_match_format_Validation_should_be_successful",
                "aaaaaaaaaaaaaaaaaaa@a.aa"
            };
        }

        private static IEnumerable<object[]> GetTestPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Create_LoginViewModel_Given_Valid_Password_min_length_and" +
                "match_format_Validation_should_be_failed",
                "Aaaaaa1@"
            };

            yield return new object[]
            {
                "Test case 2: Create_LoginViewModel_Given_Not_valid_Password_max_length_and" +
                "match_format_Validation_should_be_failed",
                "A1@aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            yield return new object[]
            {
                "Test case 3: Create_LoginViewModel_Given_Not_valid_Password_average_length_and" +
                "match_format_Validation_should_be_failed",
                "A1@aaaaaaaaaaa"
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
