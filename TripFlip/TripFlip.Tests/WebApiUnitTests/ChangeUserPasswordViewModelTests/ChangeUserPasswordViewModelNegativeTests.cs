using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.UserViewModels;

namespace WebApiUnitTests.ChangeUserPasswordViewModelTests
{
    [TestClass]
    public class ChangeUserPasswordViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestOldPasswordData), DynamicDataSourceType.Method)]
        public void TestOldPasswordValidation(
            string testCaseDisplayName,
            string oldPassword)
        {
            // Arrange
            var changeUserPasswordViewModel =
                GetChangeUserPasswordViewModel(oldPassword: oldPassword);

            // Act
            var result = Validator.TryValidateObject(changeUserPasswordViewModel,
                new ValidationContext(changeUserPasswordViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestNewPasswordData), DynamicDataSourceType.Method)]
        public void TestNewPasswordValidation(
            string testCaseDisplayName,
            string newPassword,
            string newPasswordConfirmation)
        {
            // Arrange
            var changeUserPasswordViewModel =
                GetChangeUserPasswordViewModel(newPassword: newPassword, 
                    newPasswordConfirmation: newPasswordConfirmation);

            // Act
            var result = Validator.TryValidateObject(changeUserPasswordViewModel,
                new ValidationContext(changeUserPasswordViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestNewPasswordConfirmationData), DynamicDataSourceType.Method)]
        public void TestNewPasswordConfirmationValidation(
            string testCaseDisplayName,
            string newPassword,
            string newPasswordConfirmation)
        {
            // Arrange
            var changeUserPasswordViewModel =
                GetChangeUserPasswordViewModel(newPassword: newPassword,
                    newPasswordConfirmation: newPasswordConfirmation);

            // Act
            var result = Validator.TryValidateObject(changeUserPasswordViewModel,
                new ValidationContext(changeUserPasswordViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> GetTestOldPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Test_ChangeUserPassword_Validation_Given_invalid_" +
                "OldPassword_equals_null_Validation_should_be_failed.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test_ChangeUserPassword_Validation_Given_invalid_OldPassword_equals" +
                "_empty_string_Validation_should_be_failed.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Test_ChangeUserPassword_Validation_Given_invalid_OldPassword_" +
                "too_long_length_Validation_should_be_failed.",
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaa1@" +
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaA"
            };

            yield return new object[]
            {
                "Test case 4: Test_ChangeUserPassword_Validation_Given_invalid_OldPassword_" +
                "too_short_length_Validation_should_be_failed.",
                "Aaaaa1@"
            };

            yield return new object[]
            {
                "Test case 5: Test_ChangeUserPassword_Validation_Given_invalid_OldPassword_" +
                "incorrect_format_Validation_should_be_failed.",
                new string('*', 10)
            };
        }

        static IEnumerable<object[]> GetTestNewPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Test_ChangeUserPassword_Validation_Given_invalid_" +
                "NewPassword_equals_null_Validation_should_be_failed.",
                null,
                new string('*', 10)
            };

            yield return new object[]
            {
                "Test case 2: Test_ChangeUserPassword_Validation_Given_invalid_NewPassword_equals" +
                "_empty_string_Validation_should_be_failed.",
                string.Empty,
                new string('*', 10),
            };

            yield return new object[]
            {
                "Test case 3: Test_ChangeUserPassword_Validation_Given_invalid_NewPassword_" +
                "too_long_length_Validation_should_be_failed.",
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaa1@" +
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaA",
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaa1@" +
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaa"
            };

            yield return new object[]
            {
                "Test case 4: Test_ChangeUserPassword_Validation_Given_invalid_NewPassword_" +
                "too_short_length_Validation_should_be_failed.",
                "Aaaaa1@",
                "Aaaaa1@"
            };

            yield return new object[]
            {
                "Test case 5: Test_ChangeUserPassword_Validation_Given_invalid_NewPassword_" +
                "incorrect_format_Validation_should_be_failed.",
                new string('*', 10),
                new string('*', 10)
            };
        }

        static IEnumerable<object[]> GetTestNewPasswordConfirmationData()
        {
            yield return new object[]
            {
                "Test case 1: Test_ChangeUserPassword_Validation_Given_invalid_" +
                "NewPasswordConfirmation_equals_null_Validation_should_be_failed.",
                "Aaaaa1@a",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test_ChangeUserPassword_Validation_Given_invalid_" +
                "NewPasswordConfirmation_equals_empty_string_Validation_should_be_failed.",
                "Aaaaa1@a",
                string.Empty,
            };

            yield return new object[]
            {
                "Test case 3: Test_ChangeUserPassword_Validation_Given_invalid_" +
                "NewPasswordConfirmation_too_long_length_Validation_should_be_failed.",
                "Aaaaa1@a",
                new string('*', 101)
            };

            yield return new object[]
            {
                "Test case 4: Test_ChangeUserPassword_Validation_Given_invalid_" +
                "NewPasswordConfirmation_too_short_length_Validation_should_be_failed.",
                "Aaaaa1@a",
                new string('*', 7)
            };

            yield return new object[]
            {
                "Test case 5: Test_ChangeUserPassword_Validation_Given_invalid_" +
                "NewPasswordConfirmation_missmatch_with_NewPassword_Validation_should_be_failed.",
                "Aaaaa1@a",
                new string('*', 8)
            };
        }

        private ChangeUserPasswordViewModel GetChangeUserPasswordViewModel(
            string oldPassword = "String1@string", string newPassword = "string1@String", 
            string newPasswordConfirmation = "string1@String")
        {
            return new ChangeUserPasswordViewModel()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                NewPasswordConfirmation = newPasswordConfirmation
            };
        }
    }
}
